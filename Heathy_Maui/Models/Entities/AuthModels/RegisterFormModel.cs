
using CommunityToolkit.Mvvm.ComponentModel;

namespace Healthy_MAUI.Models.Entities.AuthModel
{
    public partial class RegisterFormModel : ObservableObject
    {
        [ObservableProperty]
        public string userName = string.Empty;
        [ObservableProperty]
        public string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string confirmPassword = string.Empty;

        /// <summary>
        /// Kiểm tra xem form có hợp lệ hay không (username + password không rỗng)
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(UserName) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   !string.IsNullOrWhiteSpace(Email);
        }

        public void Reset()
        {
            UserName = string.Empty;
            Password = string.Empty;
            confirmPassword = string.Empty;
        }

    }
}
