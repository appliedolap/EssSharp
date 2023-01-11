using System.Reflection;

namespace EssSharp.Client
{
    public partial class App : Application
    {
        public App( AppShell page )
        {
            InitializeComponent();

            MainPage = page;
        }

        protected override Window CreateWindow( IActivationState activationState )
        {
            if ( base.CreateWindow(activationState) is { } window )
            {
                window.Title = Assembly.GetExecutingAssembly().GetName().Name;
                return window;
            }

            return null; ;
        }
    }
}