using System.Reflection;

using EssSharp.Client.ViewModels;

using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace EssSharp.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                });

            #if DEBUG
            {
                builder.Logging.AddDebug();
            }
            #endif

            var clientAssembly = Assembly.GetExecutingAssembly();
            using var settingsStream = clientAssembly.GetManifestResourceStream($@"{clientAssembly.GetName().Name}.appsettings.json");

            if ( settingsStream is not null )
            { 
                var config = new ConfigurationBuilder()
                    .AddJsonStream(settingsStream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
                //builder.Services.AddSingleton(config.GetSection("Settings").Get<AppSettings>());
            }

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<TreeViewEssServerViewModel>();

            return builder.Build();
        }
    }
}