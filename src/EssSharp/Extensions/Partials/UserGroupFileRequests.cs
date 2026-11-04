using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using EssSharp.Client;

namespace EssSharp.Api
{
    /// <summary>
    /// Sends the users and groups requests that the generated layer cannot express: the ones that take or answer with a
    /// file rather than JSON.
    /// </summary>
    /// <remarks>
    /// The specification gives <c>POST /users/actions/delete</c> and <c>POST /groups/actions/delete</c> no request body, and
    /// documents the import and export of users and groups as content negotiation on <c>POST /users</c>, <c>GET /users</c>,
    /// <c>POST /groups</c> and <c>GET /groups</c>. Essbase answers these with <c>application/octet-stream</c> only: a request
    /// that sends or accepts another media type is refused (415 or 406). The response is a CSV text, kept as the bytes the
    /// server sent, which the caller reads.
    /// </remarks>
    internal static class UserGroupFileRequests
    {
        private const string _octetStream = "application/octet-stream";

        /// <summary>
        /// Asynchronously posts the given file to the given path.
        /// </summary>
        /// <param name="client">The client to send the request with.</param>
        /// <param name="configuration">The configuration of the API.</param>
        /// <param name="exceptionFactory">The factory that turns an unsuccessful response into an exception.</param>
        /// <param name="path">The path of the operation.</param>
        /// <param name="operation">The operation, as the generated layer names it, such as "UsersApi.UsersAdd".</param>
        /// <param name="file">The file to send, as the request body.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken" />
        internal static async Task<ApiResponse<byte[]>> PostAsync( IAsynchronousClient client, IReadableConfiguration configuration, ExceptionFactory exceptionFactory, string path, string operation, Stream file, int operationIndex, CancellationToken cancellationToken )
        {
            if ( file is null )
                throw new ApiException(400, $"Missing required parameter 'file' when calling {operation}");

            var options = CreateOptions(configuration, operation, operationIndex);

            options.HeaderParameters.Add("Content-Type", _octetStream);
            options.Data = file;

            var response = await client.PostAsync<byte[]>(path, options, configuration, cancellationToken).ConfigureAwait(false);

            ThrowIfUnsuccessful(exceptionFactory, operation, response);

            return response;
        }

        /// <summary>
        /// Asynchronously gets the given path as a file.
        /// </summary>
        /// <param name="client">The client to send the request with.</param>
        /// <param name="configuration">The configuration of the API.</param>
        /// <param name="exceptionFactory">The factory that turns an unsuccessful response into an exception.</param>
        /// <param name="path">The path of the operation.</param>
        /// <param name="operation">The operation, as the generated layer names it, such as "UsersApi.UsersSearch".</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        /// <param name="cancellationToken" />
        internal static async Task<ApiResponse<byte[]>> GetAsync( IAsynchronousClient client, IReadableConfiguration configuration, ExceptionFactory exceptionFactory, string path, string operation, int operationIndex, CancellationToken cancellationToken )
        {
            var options = CreateOptions(configuration, operation, operationIndex);

            var response = await client.GetAsync<byte[]>(path, options, configuration, cancellationToken).ConfigureAwait(false);

            ThrowIfUnsuccessful(exceptionFactory, operation, response);

            return response;
        }

        /// <summary>
        /// Returns request options that accept a file and authenticate as the given configuration does.
        /// </summary>
        /// <param name="configuration">The configuration of the API.</param>
        /// <param name="operation">The operation, as the generated layer names it.</param>
        /// <param name="operationIndex">Index associated with the operation.</param>
        private static RequestOptions CreateOptions( IReadableConfiguration configuration, string operation, int operationIndex )
        {
            var options = new RequestOptions();

            options.HeaderParameters.Add("Accept", _octetStream);

            options.Operation      = operation;
            options.OperationIndex = operationIndex;

            // authentication (OAuth2) required
            // oauth required
            if ( !options.HeaderParameters.ContainsKey("Authorization") )
            {
                if ( !string.IsNullOrEmpty(configuration.AccessToken) )
                {
                    options.HeaderParameters.Add("Authorization", "Bearer " + configuration.AccessToken);
                }
                else if ( !string.IsNullOrEmpty(configuration.OAuthTokenUrl) &&
                          !string.IsNullOrEmpty(configuration.OAuthClientId) &&
                          !string.IsNullOrEmpty(configuration.OAuthClientSecret) &&
                          configuration.OAuthFlow != null )
                {
                    options.OAuth = true;
                }
            }

            // authentication (basicAuth) required
            // http basic authentication required
            if ( (!string.IsNullOrEmpty(configuration.Username) || !string.IsNullOrEmpty(configuration.Password)) && !options.HeaderParameters.ContainsKey("Authorization") )
                options.HeaderParameters.Add("Authorization", "Basic " + ClientUtils.Base64Encode(configuration.Username + ":" + configuration.Password));

            return options;
        }

        /// <summary>
        /// Throws the exception the given factory makes of the given response, if it makes one.
        /// </summary>
        /// <param name="exceptionFactory">The factory that turns an unsuccessful response into an exception.</param>
        /// <param name="operation">The operation, as the generated layer names it.</param>
        /// <param name="response">The response.</param>
        private static void ThrowIfUnsuccessful( ExceptionFactory exceptionFactory, string operation, IApiResponse response )
        {
            if ( exceptionFactory?.Invoke(operation.Substring(operation.IndexOf('.') + 1), response) is { } exception )
                throw exception;
        }
    }
}
