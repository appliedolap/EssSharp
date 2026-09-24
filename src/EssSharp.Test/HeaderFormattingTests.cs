using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

using RestSharp;
using Xunit;

namespace EssSharp.Test
{
    /// <summary>Tests for request and response header formatting.</summary>
    public class HeaderFormattingTests
    {
        [Fact]
        public void GetFormattedHeaders_MasksSensitiveValuesAndPreservesRepeatedHeaders()
        {
            var headers = new List<HeaderParameter>
            {
                new HeaderParameter(@"Authorization", @"Bearer"),
                new HeaderParameter(@"Authorization", @"raw-key"),
                new HeaderParameter(@"Authorization", @"sk_live_abc ******** (0 bytes)"),
                new HeaderParameter(@"Password", @"hunter(masked)2"),
                new HeaderParameter(@"Proxy-Authorization", @"Bearer x(masked)"),
                new HeaderParameter(@"Set-Cookie", @"FedAuth=first; path=/"),
                new HeaderParameter(@"Set-Cookie", @"Domain=second; path=/"),
                new HeaderParameter(@"Username", @"********"),
                new HeaderParameter(@"X-Diagnostic", @"retained"),
                new HeaderParameter(@"X-Diagnostic", @"second"),
            };

            var extensions = typeof(EssServer).Assembly.GetType(@"EssSharp.Extensions");
            var methods = extensions.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var formatter = methods
                .Single(method => method.Name == @"GetFormattedHeaders"
                    && method.GetParameters().FirstOrDefault()?.ParameterType == typeof(List<HeaderParameter>));
            var requestFormatter = methods
                .Single(method => method.Name == @"GetFormattedHeaders"
                    && method.GetParameters().FirstOrDefault()?.ParameterType == typeof(RestRequest));
            var responseFormatter = methods
                .Single(method => method.Name == @"GetFormattedHeaders"
                    && method.GetParameters().FirstOrDefault()?.ParameterType == typeof(RestResponse));

            var formatted = (string)formatter.Invoke(null, new object[] { headers, true, null, $@"{'\n'}" });
            var lines = formatted.Split('\n')
                .Select(line => Regex.Replace(line, @"^([^:]+:)\s+", @"$1 "))
                .ToArray();

            Assert.Equal(true, requestFormatter.GetParameters()[1].DefaultValue);
            Assert.Equal(true, responseFormatter.GetParameters()[1].DefaultValue);
            Assert.Contains(
                @"Authorization: Bearer ******** (0 bytes); ******** (7 bytes); ******** (30 bytes)",
                lines);
            Assert.Contains(@"Password: ********", lines);
            Assert.Contains(@"Proxy-Authorization: Bearer ******** (9 bytes)", lines);
            Assert.Contains(@"Set-Cookie: FedAuth=********; path=/", lines);
            Assert.Contains(@"Set-Cookie: Domain=********; path=/", lines);
            Assert.Contains(@"Username: ********", lines);
            Assert.Contains(@"X-Diagnostic: retained; second", lines);
            Assert.DoesNotContain(@"hunter(masked)2", formatted);
            Assert.DoesNotContain(@"x(masked)", formatted);
            Assert.DoesNotContain(@"sk_live_abc", formatted);
            Assert.DoesNotContain(@"FedAuth=first", formatted);
            Assert.DoesNotContain(@"Domain=second", formatted);
        }

        [Fact]
        public void RequestAndResponseMessages_MaskHeadersWithoutChangingBodies()
        {
            var extensions = typeof(EssServer).Assembly.GetType(@"EssSharp.Extensions");
            var methods = extensions.GetMethods(BindingFlags.NonPublic | BindingFlags.Static);
            var requestFormatter = methods.Single(method => method.Name == @"GetFormattedRequestMessage");
            var responseFormatter = methods.Single(method => method.Name == @"GetFormattedResponseMessage");

            var request = new RestRequest(@"resource", Method.Post);
            request.AddParameter(new HeaderParameter(@"Authorization", @"Bearer request-secret"));
            request.AddParameter(new HeaderParameter(@"Cookie", @"FedAuth=request-cookie"));
            request.AddStringBody(@"{""Authorization"":""body-secret""}", ContentType.Json);

            var configuration = new EssSharp.Client.Configuration
            {
                BasePath = @"https://example.test/api/",
            };

            var requestMessage = (string)requestFormatter.Invoke(null, new object[] { request, configuration });

            Assert.Contains(@"# Authorization:", requestMessage);
            Assert.Contains(@"Bearer ******** (14 bytes)", requestMessage);
            Assert.Contains(@"# Cookie:", requestMessage);
            Assert.Contains(@"FedAuth=********", requestMessage);
            Assert.Contains(@"body-secret", requestMessage);
            Assert.DoesNotContain(@"request-secret", requestMessage);
            Assert.DoesNotContain(@"request-cookie", requestMessage);

            var response = new RestResponse(request)
            {
                ResponseUri = new Uri(@"https://example.test/api/resource"),
                StatusCode = HttpStatusCode.OK,
                StatusDescription = @"OK",
                ContentType = @"application/json",
                Content = @"{""Authorization"":""body-secret""}",
                Headers = new List<HeaderParameter>
                {
                    new HeaderParameter(@"Authorization", @"Bearer response-secret"),
                    new HeaderParameter(@"Set-Cookie", @"FedAuth=response-cookie; path=/"),
                    new HeaderParameter(@"Set-Cookie", @"Domain=response-domain; path=/"),
                },
            };

            var responseMessage = (string)responseFormatter.Invoke(null, new object[] { response });

            Assert.Contains(@"# Authorization:", responseMessage);
            Assert.Contains(@"Bearer ******** (15 bytes)", responseMessage);
            Assert.Contains(@"# Set-Cookie:", responseMessage);
            Assert.Contains(@"FedAuth=********; path=/", responseMessage);
            Assert.Contains(@"Domain=********; path=/", responseMessage);
            Assert.Contains(@"body-secret", responseMessage);
            Assert.DoesNotContain(@"response-secret", responseMessage);
            Assert.DoesNotContain(@"response-cookie", responseMessage);
            Assert.DoesNotContain(@"response-domain", responseMessage);
        }
    }
}
