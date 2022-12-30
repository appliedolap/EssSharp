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
                Label.Text = "Collecting server info.";

                string providedServer = await DisplayPromptAsync("Server", "Enter a fully formed Essbase REST URL:", initialValue: server, keyboard: Keyboard.Text);

                if ( string.IsNullOrWhiteSpace(providedServer) || !Uri.TryCreate(providedServer, UriKind.Absolute, out _) )
                {
                    // Doesn't work on MacCatalyst...
                    //await DisplayAlert("Whoops", "An absolute REST URL basepath is required.", "OK");

                    Label.Text = "An absolute REST URL basepath is required";
                    return;
                }

                string providedUsername = await DisplayPromptAsync("Username", "Enter a valid Essbase user:", initialValue: username, keyboard: Keyboard.Text);
                string providedPassword = await DisplayPromptAsync("Password", "Enter the password for the Essbase user:", initialValue: password, keyboard: Keyboard.Text);

                server   = providedServer;
                username = providedUsername;
                password = providedPassword;

                var sessionApi = EssSharp.GetApi<UserSessionApi>(server, username, password, TimeSpan.FromSeconds(20));
                var session    = await sessionApi?.UserSessionGetSessionAsync(true, true);

                if ( string.IsNullOrWhiteSpace(session?.Token) )
                    throw new Exception($"Unable to get a valid session token for user {username}.");

                SemanticScreenReader.Announce($"Sucessfully obtained a session token for {username}.");

                // Doesn't work on MacCatalyst...
                //await DisplayAlert("Nice", $"Got token '{token}' for user {username}.", "OK");

                Label.Text = $"Sucessfully obtained a session token for {username}.";
            }
            catch ( Exception ex )
            {
                // Doesn't work on MacCatalyst...
                //await DisplayAlert("Whoops", ex.Message, "OK");

                try { Label.Text = ex.Message; } catch {}
            }
        }
    }
}