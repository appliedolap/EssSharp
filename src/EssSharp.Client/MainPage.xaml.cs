using EssSharp.Client.ViewModels;

namespace EssSharp.Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage( TreeViewEssServerViewModel viewModel )
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}