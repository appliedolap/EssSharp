using EssSharp.Api;
using Microsoft.Maui.Controls;

namespace EssSharp.Client
{
    public partial class MainPage : ContentPage
    {
        string server = "http://essbase-21-4:9000/essbase/rest/v1";
        string username = "admin";
        string password = "password1";

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnButtonClicked( object sender, EventArgs e )
        {
            try
            {
                string providedServer   = await DisplayPromptAsync("Server", "Enter a fully formed Essbase REST URL:", initialValue: server, keyboard: Keyboard.Text);

                if ( string.IsNullOrWhiteSpace(providedServer) )
                {
                    await DisplayAlert("Whoops", "A fully formed REST URL is required.", "OK");
                    return;
                }

                string providedUsername = await DisplayPromptAsync("Username", "Enter a valid Essbase user:", initialValue: username, keyboard: Keyboard.Text);
                string providedPassword = await DisplayPromptAsync("Password", "Enter the password for the Essbase user:", initialValue: password, keyboard: Keyboard.Text);

                server = providedServer;
                username = providedUsername;
                password = providedPassword;

                var token = (await GetConnector<UserSessionApi>().Api.UserSessionGetSessionAsync(true)).Token;

                if ( string.IsNullOrEmpty(token) )
                    throw new Exception($"Unable to get a token for user {username}.");

                SemanticScreenReader.Announce(token);
                await DisplayAlert("Nice", $"Got token '{token}' for user {username}.", "OK");
            }
            catch ( Exception ex )
            {
                await DisplayAlert("Whoops", ex.Message, "OK");
            }
        }

        /// <summary />
        /// <typeparam name="T" />
        /// <param name="client" />
        /// <param name="timeout" />
        /// <param name="callerPath" />
        /// <param name="callerName" />
        public (T Api, ApiClient Client) GetConnector<T>( IAsynchronousClient client = null, int timeout = int.MaxValue, [System.Runtime.CompilerServices.CallerFilePath] string callerPath = null, [System.Runtime.CompilerServices.CallerMemberName] string callerName = null ) where T : IApiAccessor, new()
        {
            // Throw an exception if no REST url is available.
            if ( string.IsNullOrEmpty(server) )
                throw new Exception($@"A valid {nameof(server)} must be available in order to use the {typeof(T).Name} required by {(!string.IsNullOrEmpty(callerPath) ? $@"{Path.GetFileNameWithoutExtension(callerPath)}.{callerName}".TrimEnd('.') : nameof(MainPage))}.");

            // Construct and return the requested API.
            return (new T
            {
                AsynchronousClient = client ??= GetConnectorApiClient(server),
                Configuration = new Configuration
                {
                    BasePath = server,
                    Username = username,
                    Password = password,
                    Timeout = timeout,
                    UserAgent = $@"EssSharp.Client/1.0.0.0"
                },
                ExceptionFactory = Configuration.DefaultExceptionFactory
            }, client as ApiClient);
        }

        /// <summary />
        /// <param name="restUrl" />
        private ApiClient GetConnectorApiClient( string restUrl = null ) => new ApiClient(restUrl ?? server);
    }
}