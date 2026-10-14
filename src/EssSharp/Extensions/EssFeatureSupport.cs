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
    /// The answer comes from the operations the server declares in the specification it serves and, for a feature that a
    /// deployment switches on or off, from the flags the server reports in <c>/about/instance</c>. Both are read once and
    /// cached. A flag that is reported false, or not reported at all, means the feature is not supported. When the server
    /// does not serve its specification (or does not answer <c>/about/instance</c>), that source is not used, and a feature
    /// is reported as supported until a member that needs it fails in a way that shows the server lacks it; that failure
    /// is remembered for the server.
    /// </remarks>
    internal class EssFeatureSupport
    {
        #region Private Data

        // The master switch for the AI features, which each AI capability also needs.
        private static readonly string[] _aiEnabled = new[] { "aiEnabled" };

        private static readonly Dictionary<EssFeature, FeatureDefinition> _features = new Dictionary<EssFeature, FeatureDefinition>()
        {
            [EssFeature.DrillthroughReportExecution] = new FeatureDefinition(
                description: "Drillthrough report execution",
                operations:  new[] { "POST /applications/{}/databases/{}/reports/{}" },
                statusCode:  HttpStatusCode.MethodNotAllowed),

            [EssFeature.DrillthroughReportsForCells] = new FeatureDefinition(
                description: "Getting the drillthrough reports for grid cells",
                operations:  new[] { "POST /applications/{}/databases/{}/dtreports/list" },
                statusCode:  HttpStatusCode.NotFound),

            [EssFeature.AI] = new FeatureDefinition(
                description: "AI",
                operations:  new[] { "GET /ai/connection" },
                flags:       new[] { _aiEnabled }),

            [EssFeature.AIMdxGeneration] = new FeatureDefinition(
                description: "Generating MDX with AI",
                operations:  new[] { "POST /ai/applications/{}/databases/{}/mdxgenerator" },
                flags:       new[] { _aiEnabled, new[] { "aiMdxEnabled" } }),

            // Ask Essbase has no operation of its own, so its flags alone decide.
            [EssFeature.AIAskEssbase] = new FeatureDefinition(
                description: "Ask Essbase",
                operations:  Array.Empty<string>(),
                flags:       new[] { _aiEnabled, new[] { "aiAskEssbaseEnabled" } }),

            [EssFeature.AICalcGeneration] = new FeatureDefinition(
                description: "Generating calculation scripts with AI",
                operations:  new[] { "POST /ai/applications/{}/databases/{}/calculation/generate" },
                flags:       new[] { _aiEnabled, new[] { "aiCalcEnabled" } }),

            [EssFeature.AISemanticSearch] = new FeatureDefinition(
                description: "AI semantic search",
                operations:  new[] { "POST /ai/applications/{}/databases/{}/semanticsearch" },
                flags:       new[] { _aiEnabled, new[] { "aiSemanticSearchEnabled" } }),

            // Essbase 21 reports one provisioning flag for both kinds of provisioning.
            [EssFeature.ServiceRoleProvisioning] = new FeatureDefinition(
                description: "Service role provisioning",
                operations:  new[] { "PUT /permissions/{}" },
                flags:       new[] { new[] { "serviceRoleProvisioningSupported", "provisioningSupported" } }),

            [EssFeature.ApplicationRoleProvisioning] = new FeatureDefinition(
                description: "Application role provisioning",
                operations:  new[] { "PUT /applications/{}/permissions/{}" },
                flags:       new[] { new[] { "appRoleProvisioningSupported", "provisioningSupported" } }),

            [EssFeature.PasswordReset] = new FeatureDefinition(
                description: "Password reset",
                operations:  new[] { "PUT /session/password" },
                flags:       new[] { new[] { "resetPasswordSupported" } })
        };

        private static readonly string[] _httpMethods = new[] { "get", "put", "post", "delete", "patch", "head", "options" };

        private readonly EssServer                              _server;
        private readonly ConcurrentDictionary<EssFeature, bool> _unsupportedFeatures = new ConcurrentDictionary<EssFeature, bool>();
        private readonly ReadOnce<HashSet<string>>              _declaredOperations  = new ReadOnce<HashSet<string>>();
        private readonly ReadOnce<JObject>                      _instance            = new ReadOnce<JObject>();

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

            // Without a specification, the operations cannot rule the feature out.
            if (await _declaredOperations.GetAsync(ReadDeclaredOperationsAsync, cancellationToken).ConfigureAwait(false) is { } declaredOperations && !definition.Operations.All(declaredOperations.Contains))
                return false;

            // Without the instance details, the flags cannot rule the feature out.
            if (definition.Flags.Length > 0 && await _instance.GetAsync(ReadInstanceAsync, cancellationToken).ConfigureAwait(false) is { } instance)
                return AreFlagsSet(instance, definition.Flags);

            return true;
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

            var supported = await _declaredOperations.GetAsync(ReadDeclaredOperationsAsync, cancellationToken).ConfigureAwait(false) is { } declaredOperations
                ? definition.Operations.All(declaredOperations.Contains)
                : statusCode != definition.StatusCode;

            if (supported)
                return null;

            _unsupportedFeatures[feature] = true;

            return new NotSupportedException($@"{definition.Description} is not supported by the Essbase server ""{_server.Name}"". {exception.Message}".TrimEnd(), exception);
        }

        /// <summary>
        /// Returns whether the given instance details report every one of the given flags as true, under one of its names.
        /// </summary>
        /// <param name="instance">The instance details, as reported by <c>/about/instance</c>.</param>
        /// <param name="flags">The flags, each as its current name followed by any older names.</param>
        internal static bool AreFlagsSet( JObject instance, string[][] flags ) =>
            flags.All(names => names.Any(name => instance?[name] is JValue { Type: JTokenType.Boolean, Value: true }));

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

                LogInformation(@"Read {Count} operations from the specification served by ""{Server}"".", declaredOperations.Count, _server.Name);

                return declaredOperations;
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (Exception e)
            {
                LogInformation(@"The Essbase server ""{Server}"" does not serve its specification, so its operations do not rule features out. {Message}", _server.Name, e.Message);

                return null;
            }
        }

        /// <summary>
        /// Asynchronously reads the instance details the server reports in <c>/about/instance</c>, untyped, so that a flag
        /// that is not reported is told apart from one reported false; or returns null when the server does not answer.
        /// </summary>
        /// <param name="cancellationToken" />
        private async Task<JObject> ReadInstanceAsync( CancellationToken cancellationToken )
        {
            try
            {
                var api      = _server.GetApi<AboutEssbaseApi>();
                var response = await api.AboutGetInstanceDetailsWithHttpInfoAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

                return JObject.Parse(response?.RawContent ?? string.Empty);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
            catch (Exception e)
            {
                LogInformation(@"The Essbase server ""{Server}"" does not report its instance details, so its flags do not rule features out. {Message}", _server.Name, e.Message);

                return null;
            }
        }

        /// <summary>
        /// Writes an informational log message through the server's logger, if it has one.
        /// </summary>
        /// <param name="message">Format string of the log message in message template format.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        private void LogInformation( string message, params object[] args ) =>
            _server.Configuration?.Logger?.LogInformation(new EventId(id: (int)EssSharpLogEventType.Message), message, args);

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
            /// <param name="statusCode">The status code a server that lacks the feature answers with, if known.</param>
            /// <param name="flags">The <c>/about/instance</c> flags the feature needs, each as its current name followed by any older names.</param>
            internal FeatureDefinition( string description, string[] operations, HttpStatusCode? statusCode = null, string[][] flags = null )
            {
                Description = description;
                Operations  = operations;
                StatusCode  = statusCode;
                Flags       = flags ?? Array.Empty<string[]>();
            }

            /// <summary>
            /// The feature, as it begins a sentence.
            /// </summary>
            internal string Description { get; }

            /// <summary>
            /// The <c>/about/instance</c> flags the feature needs, each as its current name followed by any older names. Every
            /// one must be reported true, under one of its names.
            /// </summary>
            internal string[][] Flags { get; }

            /// <summary>
            /// The operations the feature needs, as returned by <see cref="GetOperation(string, string)"/>.
            /// </summary>
            internal string[] Operations { get; }

            /// <summary>
            /// The status code a server that lacks the feature answers with, if known, used when the server serves no specification.
            /// </summary>
            internal HttpStatusCode? StatusCode { get; }
        }

        /// <summary>
        /// A value read from the server once, on first use, and kept for the life of the server. A cancelled read is not kept.
        /// </summary>
        /// <typeparam name="T">The type of the value; null means the server could not provide it.</typeparam>
        private sealed class ReadOnce<T> where T : class
        {
            private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

            private T             _value;
            private volatile bool _read;

            /// <summary>
            /// Asynchronously returns the value, reading it with the given <paramref name="read"/> function on first use.
            /// </summary>
            /// <param name="read">Reads the value from the server.</param>
            /// <param name="cancellationToken" />
            internal async Task<T> GetAsync( Func<CancellationToken, Task<T>> read, CancellationToken cancellationToken )
            {
                if (_read)
                    return _value;

                await _lock.WaitAsync(cancellationToken).ConfigureAwait(false);

                try
                {
                    if (!_read)
                    {
                        _value = await read(cancellationToken).ConfigureAwait(false);
                        _read  = true;
                    }

                    return _value;
                }
                finally
                {
                    _lock.Release();
                }
            }
        }

        #endregion
    }
}
