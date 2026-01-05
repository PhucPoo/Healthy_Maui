using Healthy_MAUI.ViewModels;

namespace Healthy_MAUI.Views.Auth
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
