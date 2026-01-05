using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Healthy_MAUI.Models.Entities.AuthModel;
using Heathy_Maui.Data.Interface;
using Microsoft.Maui.Controls;

namespace Healthy_MAUI.Views.Auth
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IAccountRepository _accountRepository;

        // Property bind vào Label hiển thị lỗi
        [ObservableProperty]
        private string errorMessage = string.Empty;

        public LoginFormModel Form { get; } = new();

        public LoginPageViewModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            ErrorMessage = string.Empty;

            if (!Form.IsValid())
                return;

            // Kiểm tra thông tin đăng nhập
            var isValid = await _accountRepository.ValidateCredentialsAsync(
                Form.UserName.Trim(),
                Form.Password);

            if (!isValid)
            {
                ErrorMessage = "Sai tài khoản hoặc mật khẩu";
                return;
            }

            var account = await _accountRepository.GetAccountByUsernameAsync(Form.UserName.Trim());

            if (account == null)
            {
                ErrorMessage = "Không tìm thấy tài khoản";
                return;
            }

            // Lưu thông tin đăng nhập
            Preferences.Set("IsLoggedIn", true);
            Preferences.Set("AccountId", account.Id);
            Preferences.Set("Username", account.Username);
            Preferences.Set("Email", account.Email);
            Preferences.Set("Role", account.Role?.RoleName);

            // Navigate tới Home
            await GoHomeAsync();
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            await Shell.Current.GoToAsync("register");
        }

        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            await Shell.Current.GoToAsync("forgotpassword");
        }

        // Phương thức riêng để navigate Home
        private async Task GoHomeAsync()
        {
            await Shell.Current.GoToAsync("//home");
        }
    }
}
