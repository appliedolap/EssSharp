using EssSharp.Api;
using Microsoft.Maui.Controls;

namespace EssSharp.Client
{
    public partial class MainPage : ContentPage
    {
        string server = "http://localhost:9000/essbase";
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
                Label.Text = "Collecting server info...";

                string providedServer   = await DisplayPromptAsync("Server",   "Enter an Essbase server URL:",             initialValue: server,   keyboard: Keyboard.Text);
                string providedUsername = await DisplayPromptAsync("Username", "Enter a valid Essbase user:",              initialValue: username, keyboard: Keyboard.Text);
                string providedPassword = await DisplayPromptAsync("Password", "Enter the password for the Essbase user:", initialValue: password, keyboard: Keyboard.Text);

                Label.Text = "Getting app and cube info...";

                server   = providedServer;
                username = providedUsername;
                password = providedPassword;

                var    cancellationToken    = new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token;
                string appAndCubeNames = null;

                EssServer essServer = new EssServer(server, username, password);

                foreach ( EssApplication essApplication in await essServer.GetApplicationsAsync(cancellationToken) )
                    foreach ( EssCube essCube in await essApplication.GetCubesAsync(cancellationToken) )
                        appAndCubeNames = $@"{appAndCubeNames};{essApplication.Name}.{essCube.Name}".Trim(';');

                SemanticScreenReader.Announce($"Sucessfully obtained app and cube names.");

                // Doesn't work on MacCatalyst...
                //await DisplayAlert("Nice", $"Sucessfully obtained app and cube names.", "OK");

                Label.Text = appAndCubeNames;
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