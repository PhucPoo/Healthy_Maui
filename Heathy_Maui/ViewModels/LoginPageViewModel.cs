using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Healthy_MAUI.Models.Entities.AuthModel;
using Heathy_Maui.Data.Interface;
using Heathy_Maui.Helpers;

namespace Healthy_MAUI.ViewModels
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
        private async Task Login()
        {
            ErrorMessage = string.Empty;

            // Kiểm tra form hợp lệ
            if (!Form.IsValid())
            {
                NotificationHelper.ShowNotification("Vui lòng điền đầy đủ thông tin");
                return;
            }

            try
            {
                // Kiểm tra thông tin đăng nhập
                var isValid = await _accountRepository.ValidateCredentialsAsync(
                    Form.UserName.Trim(),
                    Form.Password);

                if (!isValid)
                {
                    NotificationHelper.ShowNotification("Sai tài khoản hoặc mật khẩu");
                    return;
                }

                var account = await _accountRepository.GetAccountByUsernameAsync(Form.UserName.Trim());
                if (account == null)
                {
                    NotificationHelper.ShowNotification("Không tìm thấy tài khoản");
                    return;
                }

                // Lưu thông tin đăng nhập
                Preferences.Set("IsLoggedIn", true);
                Preferences.Set("AccountId", account.Id);
                Preferences.Set("Username", account.Username);
                Preferences.Set("FullName", account.Username); // Hoặc lấy từ account.FullName nếu có
                Preferences.Set("Email", account.Email);
                Preferences.Set("Role", account.Role?.RoleName);
                Preferences.Set("AvatarUrl", ""); // Thêm avatar URL nếu có

                // Gửi message để MainPage cập nhật UI
                WeakReferenceMessenger.Default.Send(new LoginMessage());

                // Thông báo đăng nhập thành công
                NotificationHelper.ShowNotification($"Đăng nhập thành công: {account.Username}");

                // Navigate về MainPage
                await Shell.Current.GoToAsync("///MainPage");
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification($"Lỗi hệ thống: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task Register()
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }

        [RelayCommand]
        private async Task ForgotPassword()
        {
            await Shell.Current.GoToAsync("///ForgotPasswordPage");
        }

        [RelayCommand]
        private async Task Back()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
    }

    // Define the message class
    public class LoginMessage
    {
    }
}