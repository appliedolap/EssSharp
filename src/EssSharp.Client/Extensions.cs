using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using EssSharp.Client;
using Microsoft.Maui.Controls;

namespace EssSharp.Client
{
    /// <summary>
    /// Extension methods leveraged by the sample EssSharp client.
    /// </summary>
    public static class Extensions
    {
        #region System.Threading.Tasks.Task

        /// <summary>
        /// An extension overload that allows a timeout to be applied to an awaited <see cref="Task"/>.
        /// </summary>
        /// <param name="task">The extended <see cref="Task"/>.</param>
        /// <param name="timeout">The amount of time to wait before timing out the task.</param>
        /// <returns></returns>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            using (var timeoutCancellationSource = new CancellationSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationSource.Token));

                if (completedTask == task)
                {
                    // Cancel the timeout task...
                    timeoutCancellationSource.Cancel();
                    // Return the original task result, which allows 
                    // for result and exception propagation.
                    await task;
                }
                else
                {
                    throw new TimeoutException();
                }
            }
        }

        /// <summary>
        /// An extension overload that allows a timeout to be applied to an awaited <see cref="Task"/>.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task">The extended <see cref="Task"/>.</param>
        /// <param name="timeout">The amount of time to wait before timing out the task.</param>
        /// <returns></returns>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using (var timeoutCancellationSource = new CancellationSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationSource.Token));

                if (completedTask == task)
                {
                    // Cancel the timeout task...
                    timeoutCancellationSource.Cancel();
                    // Return the original task result, which allows 
                    // for result and exception propagation.
                    return await task;
                }
                else
                {
                    throw new TimeoutException();
                }
            }
        }

        #endregion
    }
}

#region System.Threading + CancellationSource

namespace EssSharp
{
    public static class EssSharp
    {
        /// <summary>
        /// A rudimentary client pool implementation.
        /// </summary>
        private static ConcurrentDictionary<string, ApiClient> ClientPool { get; set; } = new ConcurrentDictionary<string, ApiClient>();

        /// <summary />
        /// <typeparam name="T" />
        /// <param name="basePath" />
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="timeout" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static T GetApi<T>(string basePath, string username, string password, TimeSpan? timeout = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null) where T : IApiAccessor, new() =>
            GetApiAndClient<T>(new Configuration() { BasePath = basePath, Username = username, Password = password, Timeout = (timeout ?? TimeSpan.FromMilliseconds(int.MaxValue)).Milliseconds, UserAgent = "EssSharp.Client/1.0.0.0"}, null, callerPath, callerName).Api;

        /// <summary />
        /// <typeparam name="T" />
        /// <param name="basePath" />
        /// <param name="username" />
        /// <param name="password" />
        /// <param name="timeout" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static (T Api, ApiClient Client) GetApiAndClient<T>(string basePath, string username, string password, TimeSpan? timeout = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null) where T : IApiAccessor, new() =>
            GetApiAndClient<T>(new Configuration() { BasePath = basePath, Username = username, Password = password, Timeout = (timeout ?? TimeSpan.FromMilliseconds(int.MaxValue)).Milliseconds, UserAgent = "EssSharp.Client/1.0.0.0" }, null, callerPath, callerName);

        /// <summary />
        /// <typeparam name="T" />
        /// <param name="configuration" />
        /// <param name="client" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static T GetApi<T>(Configuration configuration, ApiClient client = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null) where T : IApiAccessor, new() =>
            GetApiAndClient<T>(configuration, client, callerPath, callerName).Api;


        /// <summary />
        /// <typeparam name="T" />
        /// <param name="configuration" />
        /// <param name="client" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public static (T Api, ApiClient Client) GetApiAndClient<T>(Configuration configuration, ApiClient client = null, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null) where T : IApiAccessor, new()
        {
            // Throw an exception if no REST url is available.
            if ( string.IsNullOrEmpty(configuration?.BasePath) )
                throw new Exception($@"A valid REST endpoint must be provided in order to use the {typeof(T).Name} required by {(!string.IsNullOrEmpty(callerPath) ? $@"{Path.GetFileNameWithoutExtension(callerPath)}.{callerName}".TrimEnd('.') : nameof(MainPage))}.");

            // Construct and return the requested API and client.
            return (new T
            {
                AsynchronousClient = client ??= new ApiClient(configuration.BasePath),
                Configuration      = configuration,
                ExceptionFactory   = Configuration.DefaultExceptionFactory
            }, client);
        }
    }
}

namespace System.Threading
{
    /// <summary />
    public class CancellationSource : CancellationTokenSource
    {
        /// <summary />
        /// <param name="isUserCancellable" />
        public CancellationSource( bool isUserCancellable = true ) : base()
        {
            IsUserCancellable = isUserCancellable;
        }

        /// <summary />
        public bool IsDisposed { get; private set; }

        /// <summary />
        public bool IsUserCancellable { get; set; } = true;

        /// <summary>
        /// Gets the <see cref="CancellationToken"/> associated with this <see cref="CancellationSource"/>.
        /// </summary>
        /// <exception cref="OperationCanceledException" />
        public new CancellationToken Token
        {
            get
            {
                if ( IsCancellationRequested && IsDisposed )
                    throw new OperationCanceledException();

                return base.Token;
            }
        }

        /// <summary />
        /// <param name="disposing" />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                IsDisposed = true;
        }
    }
}

#endregion