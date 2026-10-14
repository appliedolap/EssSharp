using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace EssSharp
{
    /// <summary>
    /// Answers whether an Essbase server supports an <see cref="EssFeature"/>, and recognizes the failures that mean it does not.
    /// </summary>
    /// <remarks>
    /// The answer comes from the operations the server declares in the specification it serves, read once and cached. When
    /// the server does not serve its specification, a feature is reported as supported until a member that needs it fails
    /// in a way that shows the server lacks it, and that failure is remembered for the server.
    /// </remarks>
    internal class EssFeatureSupport
    {
        #region Private Data

        private static readonly Dictionary<EssFeature, FeatureDefinition> _features = new Dictionary<EssFeature, FeatureDefinition>()
        {
            [EssFeature.DrillthroughReportExecution] = new FeatureDefinition(
                description: "Drillthrough report execution",
                operations:  new[] { "POST /applications/{}/databases/{}/reports/{}" },
                statusCode:  HttpStatusCode.MethodNotAllowed),

            [EssFeature.DrillthroughReportsForCells] = new FeatureDefinition(
                description: "Getting the drillthrough reports for grid cells",
                operations:  new[] { "POST /applications/{}/databases/{}/dtreports/list" },
                statusCode:  HttpStatusCode.NotFound)
        };

        private static readonly string[] _httpMethods = new[] { "get", "put", "post", "delete", "patch", "head", "options" };

        private readonly EssServer                              _server;
        private readonly ConcurrentDictionary<EssFeature, bool> _unsupportedFeatures = new ConcurrentDictionary<EssFeature, bool>();
        private readonly SemaphoreSlim                          _specificationLock   = new SemaphoreSlim(1, 1);

        private HashSet<string> _declaredOperations;
        private volatile bool   _specificationRead;

        #endregion

        #region Constructors

        /// <summary />
        /// <param name="server">The server whose features to answer for.</param>
        internal EssFeatureSupport( EssServer server )
        {
            _server = server ?? throw new ArgumentNullException(nameof(server), $"An initialized {nameof(EssServer)} is required.");
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Asynchronously returns whether the server supports the given <paramref name="feature"/>.
        /// </summary>
        /// <param name="feature">The feature to check.</param>
        /// <param name="cancellationToken" />
        internal async Task<bool> SupportsAsync( EssFeature feature, CancellationToken cancellationToken = default )
        {
            var definition = GetDefinition(feature);

            if (_unsupportedFeatures.ContainsKey(feature))
                return false;

            // Without a specification, report the feature as supported until a member that needs it has failed.
            if (await GetDeclaredOperationsAsync(cancellationToken).ConfigureAwait(false) is not { } declaredOperations)
                return true;

            return definition.Operations.All(declaredOperations.Contains);
        }

        /// <summary>
        /// Asynchronously returns a <see cref="NotSupportedException"/> when the given <paramref name="exception"/>, thrown
        /// by a member that needs the given <paramref name="feature"/>, shows that the server does not support the feature;
        /// otherwise, returns null.
        /// </summary>
        /// <param name="feature">The feature the failed member needs.</param>
        /// <param name="exception">The exception the member caught.</param>
        /// <param name="cancellationToken" />
        /// <remarks>
        /// Only a 404 (not found) or 405 (method not allowed) response can mean that the server lacks an operation. Where
        /// the server serves its specification, the specification decides, so a 404 for a missing object is never mistaken
        /// for a missing operation; otherwise, the status code the server answers with when it lacks the feature decides.
        /// </remarks>
        internal async Task<NotSupportedException> GetNotSupportedExceptionAsync( EssFeature feature, Exception exception, CancellationToken cancellationToken = default )
        {
            var definition = GetDefinition(feature);

            if (GetStatusCode(exception) is not { } statusCode || statusCode is not (HttpStatusCode.NotFound or HttpStatusCode.MethodNotAllowed))
                return null;

            var supported = await GetDeclaredOperationsAsync(cancellationToken).ConfigureAwait(false) is { } declaredOperations
                ? definition.Operations.All(declaredOperations.Contains)
                : statusCode != definition.StatusCode;

            if (supported)
                return null;

            _unsupportedFeatures[feature] = true;

            return new NotSupportedException($@"{definition.Description} is not supported by the Essbase server ""{_server.Name}"". {exception.Message}".TrimEnd(), exception);
        }

        /// <summary>
        /// Returns the operation, as compared with a feature's operations, that the given HTTP <paramref name="method"/> and
        /// <paramref name="path"/> template identify. Path parameter names are removed, since servers name them differently.
        /// </summary>
        /// <param name="method">The HTTP method, such as "post".</param>
        /// <param name="path">The path template, such as "/applications/{applicationName}".</param>
        internal static string GetOperation( string method, string path ) =>
            $"{method?.ToUpperInvariant()} {Regex.Replace(path ?? string.Empty, @"\{[^}]*\}", "{}")}";

        /// <summary>
        /// Returns the operations the given specification declares, as compared with a feature's operations.
        /// </summary>
        /// <param name="specification">The OpenAPI (or Swagger) document, as JSON text.</param>
        internal static HashSet<string> GetDeclaredOperations( string specification )
        {
            if (JObject.Parse(specification ?? string.Empty)["paths"] is not JObject paths)
                throw new Exception("The specification declares no paths.");

            var declaredOperations = new HashSet<string>(StringComparer.Ordinal);

            foreach (var path in paths.Properties().Where(path => path.Value is JObject))
            {
                foreach (var method in ((JObject)path.Value).Properties().Select(operation => operation.Name).Where(name => _httpMethods.Contains(name, StringComparer.OrdinalIgnoreCase)))
                    declaredOperations.Add(GetOperation(method, path.Name));
            }

            return declaredOperations;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the definition of the given <paramref name="feature"/>.
        /// </summary>
        /// <param name="feature">The feature.</param>
        private static FeatureDefinition GetDefinition( EssFeature feature ) =>
            _features.TryGetValue(feature, out var definition) ? definition : throw new ArgumentOutOfRangeException(nameof(feature), feature, $"{feature} is not a known {nameof(EssFeature)}.");

        /// <summary>
        /// Returns the HTTP status code of the response the given <paramref name="exception"/> (or one of its inner exceptions) reports, if any.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private static HttpStatusCode? GetStatusCode( Exception exception )
        {
            for (var current = exception; current is not null; current = current.InnerException)
            {
                if (current is WebException { Response: WebExceptionRestResponse { StatusCode: { } statusCode } })
                    return statusCode;

                if (current is ApiException { ErrorCode: > 0 } apiException)
                    return (HttpStatusCode)apiException.ErrorCode;
            }

            return null;
        }

        /// <summary>
        /// Asynchronously returns the operations the server declares, or null when the server does not serve its specification.
        /// The specification is read once; a cancelled read is not remembered.
        /// </summary>
        /// <param name="cancellationToken" />
        private async Task<HashSet<string>> GetDeclaredOperationsAsync( CancellationToken cancellationToken )
        {
            if (_specificationRead)
                return _declaredOperations;

            await _specificationLock.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                if (!_specificationRead)
                {
                    _declaredOperations = await ReadDeclaredOperationsAsync(cancellationToken).ConfigureAwait(false);
                    _specificationRead  = true;
                }

                return _declaredOperations;
            }
            finally
            {
                _specificationLock.Release();
            }
        }

        /// <summary>
        /// Asynchronously reads the operations the server declares in the specification it serves, or returns null when it
        /// does not serve one.
        /// </summary>
        /// <param name="cancellationToken" />
        private async Task<HashSet<string>> ReadDeclaredOperationsAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api      = _server.GetApi<AboutEssbaseApi>();
                var response = await api.AboutGetSpecificationWithHttpInfoAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

                var declaredOperations = GetDeclaredOperations(response?.Data);

                _server.Configuration?.Logger?.LogInformation(new EventId(id: (int)EssSharpLogEventType.Message),
                    @"Read {Count} operations from the specification served by ""{Server}"".", declaredOperations.Count, _server.Name);

                return declaredOperations;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (Exception e)
            {
                _server.Configuration?.Logger?.LogInformation(new EventId(id: (int)EssSharpLogEventType.Message),
                    @"The Essbase server ""{Server}"" does not serve its specification, so features are reported as supported until they fail. {Message}", _server.Name, e.Message);

                return null;
            }
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// What an <see cref="EssFeature"/> needs from a server, and how its absence shows.
        /// </summary>
        private sealed class FeatureDefinition
        {
            /// <summary />
            /// <param name="description">The feature, as it begins a sentence.</param>
            /// <param name="operations">The operations the feature needs.</param>
            /// <param name="statusCode">The status code a server that lacks the feature answers with.</param>
            internal FeatureDefinition( string description, string[] operations, HttpStatusCode statusCode )
            {
                Description = description;
                Operations  = operations;
                StatusCode  = statusCode;
            }

            /// <summary>
            /// The feature, as it begins a sentence.
            /// </summary>
            internal string Description { get; }

            /// <summary>
            /// The operations the feature needs, as returned by <see cref="GetOperation(string, string)"/>.
            /// </summary>
            internal string[] Operations { get; }

            /// <summary>
            /// The status code a server that lacks the feature answers with, used when the server serves no specification.
            /// </summary>
            internal HttpStatusCode StatusCode { get; }
        }

        #endregion
    }
}
