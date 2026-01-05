//using CommunityToolkit.Mvvm.Input;
//using HealthManagement_MAUI.Models.Entities.AuthModel;

//namespace HealthManagement_MAUI.Features.Auth
//{
//    public partial class LoginPageViewModel : BaseViewModel
//    {
//        private readonly IAccountRepository _accountRepository;
//        private readonly IDialogService _dialogService;

//        public LoginFormModel Form { get; } = new();

//        public LoginPageViewModel(IAccountRepository accountRepository, IAppNavigator appNavigator)
//            : base(appNavigator)
//        {
//            _accountRepository = accountRepository;
//        }

//        [RelayCommand]
//        private async Task LoginAsync()
//        {
//            if (!Form.IsValid())
//                return;

//            var isValid = await _accountRepository.ValidateCredentialsAsync(
//                Form.UserName.Trim(),
//                Form.Password);

//            if (!isValid)
//            {
//                await _dialogService.ShowAlert("Lỗi", "Sai tài khoản hoặc mật khẩu", "OK");
//                return;
//            }

//            var account = await _accountRepository.GetAccountByUsernameAsync(Form.UserName.Trim());

//            if (account == null)
//            {
//                await _dialogService.ShowAlert("Lỗi", "Không tìm thấy tài khoản", "OK");
//                return;
//            }

//            Preferences.Set("IsLoggedIn", true);
//            Preferences.Set("AccountId", account.Id);
//            Preferences.Set("Username", account.Username);
//            Preferences.Set("Email", account.Email);
//            Preferences.Set("Role", account.Role?.RoleName);

//            await GoHomeAsync();
//        }

//        [RelayCommand]
//        private Task RegisterAsync() => AppNavigator.NavigateAsync(AppRoutes.RegisterPage);

//        [RelayCommand]
//        private Task ForgotPasswordAsync() => AppNavigator.NavigateAsync(AppRoutes.ForgotPassword);

//        private Task GoHomeAsync() => AppNavigator.NavigateAsync(AppRoutes.Home);
//    }
//}
