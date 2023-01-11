namespace EssSharp.Client
{
    public partial class App : Application
    {
        public App( AppShell page )
        {
            InitializeComponent();

            MainPage = page;
        }
    }
}