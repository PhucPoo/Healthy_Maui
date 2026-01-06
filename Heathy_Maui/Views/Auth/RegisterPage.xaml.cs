
using Healthy_MAUI.ViewModels;

namespace Healthy_MAUI.Views.Auth
{
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage(RegisterPageViewModel vm)
		{
            InitializeComponent();
			BindingContext = vm;
		}
	}
}