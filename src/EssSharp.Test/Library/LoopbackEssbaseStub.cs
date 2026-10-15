using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using EssSharp.Api;
using EssSharp.Client;

using Xunit;

namespace EssSharp.Test.Library
{
    /// <summary>
    /// A minimal loopback HTTP server that answers every request with a small JSON body and scripted
    /// Set-Cookie headers emitted verbatim, so malformed cookie values can be exercised byte-for-byte
    /// on the wire. Connections are handled concurrently, and responses can be held until a given
    /// number of requests have arrived to force genuine request concurrency.
    /// </summary>
    internal sealed class LoopbackEssbaseStub : IDisposable
    {
        private readonly TcpListener _listener;
        private volatile bool _disposed;

        /// <summary />
        /// <param name="setCookieHeaders">The Set-Cookie header values to emit verbatim on every response.</param>
        public LoopbackEssbaseStub( params string[] setCookieHeaders )
            : this(_ => setCookieHeaders) { }

        /// <summary />
        /// <param name="setCookieProvider">Returns the Set-Cookie header values to emit for the request at a given index.</param>
        public LoopbackEssbaseStub( Func<int, string[]> setCookieProvider )
        {
            SetCookieProvider = setCookieProvider ?? (_ => Array.Empty<string>());

            _listener = new TcpListener(IPAddress.Loopback, 0);
            _listener.Start();

            BasePath = $"http://{IPAddress.Loopback}:{((IPEndPoint)_listener.LocalEndpoint).Port}/essbase/rest/v1";

            _ = Task.Run(AcceptLoopAsync);
        }

        /// <summary>
        /// The stub base path, e.g. http://127.0.0.1:{port}/essbase/rest/v1.
        /// </summary>
        public string BasePath { get; }

        /// <summary>
        /// Holds every response until at least this many requests have arrived (0 = respond immediately).
        /// </summary>
        public int HoldResponsesUntilRequestCount { get; set; }

        /// <summary>
        /// The raw text of each received request, in order of arrival.
        /// </summary>
        public List<string> Requests { get; } = new List<string>();

        /// <summary>
        /// Returns the status code and JSON body to answer a request with, given the request line (for example,
        /// "GET /essbase/rest/v1/about HTTP/1.1"), or null to answer with the default 200 response.
        /// </summary>
        public Func<string, (HttpStatusCode StatusCode, string Body)?> ResponseProvider { get; set; }

        /// <summary>
        /// The Content-Type of every response (application/json by default).
        /// </summary>
        public string ContentType { get; set; } = "application/json";

        /// <summary>
        /// Returns the Set-Cookie header values to emit for the request at a given index.
        /// </summary>
        public Func<int, string[]> SetCookieProvider { get; }

        /// <inheritdoc />
        public void Dispose()
        {
            _disposed = true;

            try { _listener.Stop(); } catch { /* ignored */ }
        }

        /// <summary>
        /// Accepts connections until disposed, handling each concurrently.
        /// </summary>
        private async Task AcceptLoopAsync()
        {
            while ( !_disposed )
            {
                TcpClient client = null;

                try
                {
                    client = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);
                }
                catch
                {
                    client?.Dispose();
                    break;
                }

                _ = Task.Run(() => HandleConnectionAsync(client));
            }
        }

        /// <summary>
        /// Reads one request from the given connection and answers it.
        /// </summary>
        /// <param name="client">The accepted connection.</param>
        private async Task HandleConnectionAsync( TcpClient client )
        {
            try
            {
                using var stream = client.GetStream();

                // Read the request through its headers and any body.
                var request = await ReadRequestAsync(stream).ConfigureAwait(false);

                if ( string.IsNullOrEmpty(request) )
                    return;

                int index;

                lock ( Requests )
                {
                    Requests.Add(request);
                    index = Requests.Count - 1;
                }

                // Hold the response until the configured number of requests has arrived.
                while ( !_disposed && HoldResponsesUntilRequestCount > 0 )
                {
                    lock ( Requests )
                    {
                        if ( Requests.Count >= HoldResponsesUntilRequestCount )
                            break;
                    }

                    await Task.Delay(10).ConfigureAwait(false);
                }

                // Answer with the scripted response, or with a small JSON body that deserializes as a UserBean (or is ignored).
                var requestLine = request.Substring(0, Math.Max(0, request.IndexOf("\r\n", StringComparison.Ordinal)));
                var (statusCode, body) = ResponseProvider?.Invoke(requestLine) ?? (HttpStatusCode.OK, @"{""id"":""admin"",""name"":""admin"",""token"":""stub-token""}");

                var builder = new StringBuilder()
                    .Append($"HTTP/1.1 {(int)statusCode} {statusCode}\r\n")
                    .Append($"Content-Type: {ContentType}\r\n")
                    .Append($"Content-Length: {Encoding.UTF8.GetByteCount(body)}\r\n");

                foreach ( var setCookieHeader in SetCookieProvider(index) ?? Array.Empty<string>() )
                    builder.Append($"Set-Cookie: {setCookieHeader}\r\n");

                var bytes = Encoding.UTF8.GetBytes(builder.Append("Connection: close\r\n\r\n").Append(body).ToString());

                await stream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                await stream.FlushAsync().ConfigureAwait(false);
            }
            catch
            {
                // Ignore failed connections; the test assertions will surface any missing exchange.
            }
            finally
            {
                client?.Dispose();
            }
        }

        /// <summary>
        /// Reads a request through its headers and, when a Content-Length is present, its body.
        /// </summary>
        /// <param name="stream">The connection stream.</param>
        private static async Task<string> ReadRequestAsync( NetworkStream stream )
        {
            var buffer  = new byte[8192];
            var builder = new StringBuilder();

            // Read through the end of the headers.
            while ( builder.ToString().IndexOf("\r\n\r\n", StringComparison.Ordinal) < 0 )
            {
                var read = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false);

                if ( read <= 0 )
                    return builder.ToString();

                builder.Append(Encoding.ASCII.GetString(buffer, 0, read));
            }

            var text        = builder.ToString();
            var headerEnd   = text.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4;
            var contentLine = text.Split(new[] { "\r\n" }, StringSplitOptions.None).FirstOrDefault(line => line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase));

            // Drain any remaining body bytes indicated by a Content-Length header, so the connection
            // is not closed with unread data (which can reset the client).
            if ( contentLine?.Split(':') is { Length: 2 } parts && int.TryParse(parts[1].Trim(), out var contentLength) )
            {
                var remaining = contentLength - Encoding.ASCII.GetByteCount(text.Substring(headerEnd));

                while ( remaining > 0 )
                {
                    var read = await stream.ReadAsync(buffer, 0, Math.Min(buffer.Length, remaining)).ConfigureAwait(false);

                    if ( read <= 0 )
                        break;

                    builder.Append(Encoding.ASCII.GetString(buffer, 0, read));
                    remaining -= read;
                }
            }

            return builder.ToString();
        }
    }
}
