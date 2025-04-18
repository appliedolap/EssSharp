using System.Reflection;

namespace EssSharp.Client
{
    public partial class App : Application
    {
        private readonly AppShell _appShell;

        public App( AppShell page )
        {
            InitializeComponent();

            _appShell = page;
        }


        protected override Window CreateWindow( IActivationState activationState ) => new Window()
        {
            Page  = _appShell,
            Title = Assembly.GetExecutingAssembly().GetName().Name 
        };
    }
}