using EssSharp.Client.ViewModels;

namespace EssSharp.Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage( EssServerTreeViewModel viewModel )
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}