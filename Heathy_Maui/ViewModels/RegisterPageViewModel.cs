using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthManagement_MAUI.Models.Entities;
using Healthy_MAUI.Models.Entities.AuthModel;
using Heathy_Maui.Data.Interface;
using Heathy_Maui.Helpers;

namespace Healthy_MAUI.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        private readonly IAccountRepository _accountRepository;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public RegisterFormModel Form { get; } = new();

        public RegisterPageViewModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            ErrorMessage = string.Empty;

            // Kiểm tra form
            if (!Form.IsValid())
            {
                NotificationHelper.ShowNotification("Vui lòng điền đầy đủ thông tin");
                return;
            }

            if (Form.Password != Form.ConfirmPassword)
            {
                NotificationHelper.ShowNotification("Mật khẩu xác nhận không trùng khớp");
                return;
            }

            try
            {
                var account = new Account
                {
                    Username = Form.UserName.Trim(),
                    Email = Form.Email.Trim(),
                    Password = Form.Password
                };

                await _accountRepository.RegisterAsync(account);

                NotificationHelper.ShowNotification($"Đăng ký thành công: {account.Username}");

                // Navigate tới LoginPage sau khi đăng ký
                await Shell.Current.GoToAsync("///LoginPage");
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification($"Lỗi: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task BackAsync()
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
