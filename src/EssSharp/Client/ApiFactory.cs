using System;
using System.IO;

using EssSharp.Client;

namespace EssSharp
{
    internal class ApiFactory
    {
        /// <summary />
        /// <typeparam name="T" />
        /// <param name="basePath" />
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="timeout" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static T GetApi<T>( string basePath, string username, string password, TimeSpan? timeout = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null ) where T : IApiAccessor, new() =>
            GetApiAndClient<T>(new Configuration() { BasePath = basePath, Username = username, Password = password, Timeout = (timeout ?? TimeSpan.FromMilliseconds(int.MaxValue)).Milliseconds, UserAgent = "EssSharp.Client/1.0.0.0" }, null, callerPath, callerName).Api;

        /// <summary />
        /// <typeparam name="T" />
        /// <param name="basePath" />
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="timeout" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static (T Api, ApiClient Client) GetApiAndClient<T>( string basePath, string username, string password, TimeSpan? timeout = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null ) where T : IApiAccessor, new() =>
            GetApiAndClient<T>(new Configuration() { BasePath = basePath, Username = username, Password = password, Timeout = (timeout ?? TimeSpan.FromMilliseconds(int.MaxValue)).Milliseconds, UserAgent = "EssSharp.Client/1.0.0.0" }, null, callerPath, callerName);

        /// <summary />
        /// <typeparam name="T" />
        /// <param name="configuration" />
        /// <param name="client" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static T GetApi<T>( Configuration configuration, ApiClient client = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null ) where T : IApiAccessor, new() =>
            GetApiAndClient<T>(configuration, client, callerPath, callerName).Api;


        /// <summary />
        /// <typeparam name="T" />
        /// <param name="configuration" />
        /// <param name="client" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static (T Api, ApiClient Client) GetApiAndClient<T>( Configuration configuration, ApiClient client = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null ) where T : IApiAccessor, new()
        {
            // Throw an exception if no REST url is available.
            if ( string.IsNullOrEmpty(configuration?.BasePath) )
                throw new Exception($@"A valid REST endpoint must be provided in order to use the {typeof(T).Name} required by {(!string.IsNullOrEmpty(callerPath) ? $@"{Path.GetFileNameWithoutExtension(callerPath)}.{callerName}".TrimEnd('.') : nameof(EssSharp))}.");

            // Construct and return the requested API and client.
            return (new T
            {
                AsynchronousClient = client ??= new ApiClient(configuration.BasePath),
                Client             = client,
                Configuration      = configuration,
                ExceptionFactory   = Configuration.DefaultExceptionFactory
            }, client);
        }
    }
}
