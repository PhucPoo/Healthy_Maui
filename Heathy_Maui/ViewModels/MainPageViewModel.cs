using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Heathy_Maui.Helpers;
using CommunityToolkit.Mvvm.Messaging;

namespace Healthy_MAUI.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    #region Banner Properties
    [ObservableProperty]
    private int currentBannerIndex = 0;

    [ObservableProperty]
    private string currentBannerText = "Chăm sóc sức khỏe mỗi ngày";

    [ObservableProperty]
    private string currentBannerBackground = "#60A5FA";

    [ObservableProperty]
    private double indicator1Opacity = 1;

    [ObservableProperty]
    private double indicator2Opacity = 0.5;

    [ObservableProperty]
    private double indicator3Opacity = 0.5;

    private readonly string[] banners =
    {
        "Chăm sóc sức khỏe mỗi ngày",
        "Ăn uống lành mạnh, sống vui khỏe",
        "Tập luyện đều đặn, cơ thể dẻo dai"
    };

    private readonly string[] bannerColors =
    {
        "#60A5FA",
        "#4ADE80",
        "#C084FC"
    };

    private IDispatcherTimer? bannerTimer;
    #endregion

    #region Login/Avatar
    [ObservableProperty]
    private bool isLoggedIn = false;

    [ObservableProperty]
    private bool isLoginButtonVisible = true;

    [ObservableProperty]
    private bool isAvatarVisible = false;

    [ObservableProperty]
    private string loginButtonText = "ĐĂNG NHẬP";

    [ObservableProperty]
    private string avatarText = "U";

    [ObservableProperty]
    private string userName = "";

    [ObservableProperty]
    private string avatarUrl = "";

    [ObservableProperty]
    private bool hasAvatarImage = false;

    [ObservableProperty]
    private bool hasNoAvatarImage = true;
    #endregion

    public MainPageViewModel()
    {
        // Kiểm tra trạng thái đăng nhập khi khởi tạo
        CheckLoginStatus();

        // Đăng ký nhận message khi đăng nhập thành công
        WeakReferenceMessenger.Default.Register<LoginMessage>(this, (r, m) =>
        {
            CheckLoginStatus();
        });
    }

    #region Login Status Management

    /// <summary>
    /// Kiểm tra trạng thái đăng nhập từ Preferences
    /// </summary>
    private void CheckLoginStatus()
    {
        IsLoggedIn = Preferences.Get("IsLoggedIn", false);

        if (IsLoggedIn)
        {
            UserName = Preferences.Get("FullName", "User");
            AvatarUrl = Preferences.Get("AvatarUrl", string.Empty);
            UpdateUIAfterLogin(UserName, AvatarUrl);
        }
        else
        {
            // Reset về trạng thái chưa đăng nhập
            IsLoginButtonVisible = true;
            IsAvatarVisible = false;
        }
    }

    /// <summary>
    /// Cập nhật giao diện sau khi đăng nhập thành công
    /// </summary>
    public void UpdateUIAfterLogin(string userName, string avatarUrl = "")
    {
        IsLoggedIn = true;
        UserName = userName;
        AvatarUrl = avatarUrl;

        // Kiểm tra có ảnh avatar không
        HasAvatarImage = !string.IsNullOrEmpty(avatarUrl);
        HasNoAvatarImage = string.IsNullOrEmpty(avatarUrl);

        // Ẩn nút đăng nhập, hiển thị avatar
        IsLoginButtonVisible = false;
        IsAvatarVisible = true;

        // Lấy ký tự đầu tiên của tên làm avatar text (nếu không có ảnh)
        if (string.IsNullOrEmpty(avatarUrl))
        {
            AvatarText = string.IsNullOrEmpty(userName)
                ? "U"
                : userName.Trim().Substring(0, 1).ToUpper();
        }
    }

    /// <summary>
    /// Đăng xuất và reset giao diện
    /// </summary>
    private void Logout()
    {
        IsLoggedIn = false;
        IsLoginButtonVisible = true;
        IsAvatarVisible = false;
        UserName = "";
        AvatarText = "U";
        AvatarUrl = "";

        // Xóa tất cả thông tin đăng nhập
        Preferences.Clear();

        NotificationHelper.ShowNotification("Đã đăng xuất thành công!");
    }

    #endregion

    #region Banner Animation
    public void AnimateBanner()
    {
        CurrentBannerIndex = (CurrentBannerIndex + 1) % banners.Length;
        CurrentBannerText = banners[CurrentBannerIndex];
        CurrentBannerBackground = bannerColors[CurrentBannerIndex];

        Indicator1Opacity = CurrentBannerIndex == 0 ? 1 : 0.5;
        Indicator2Opacity = CurrentBannerIndex == 1 ? 1 : 0.5;
        Indicator3Opacity = CurrentBannerIndex == 2 ? 1 : 0.5;
    }

    public void StartBannerAnimation(IDispatcher dispatcher)
    {
        bannerTimer = dispatcher.CreateTimer();
        bannerTimer.Interval = TimeSpan.FromSeconds(3);
        bannerTimer.Tick += (s, e) => AnimateBanner();
        bannerTimer.Start();
    }

    public void StopBannerAnimation()
    {
        bannerTimer?.Stop();
    }
    #endregion

    #region Commands

    [RelayCommand]
    private async Task Login()
    {
        if (!IsLoggedIn)
        {
            // Chưa login, navigate tới LoginPage
            await Shell.Current.GoToAsync("///LoginPage");
        }
        else
        {
            // Đã login → hiện menu với các tùy chọn
            string fullName = Preferences.Get("FullName", "Người dùng");

            var action = await Application.Current.MainPage.DisplayActionSheet(
                fullName,
                "Hủy",
                null,
                "Xem hồ sơ",
                "Đăng xuất"
            );

            if (action == "Đăng xuất")
            {
                Logout();
            }
            else if (action == "Xem hồ sơ")
            {
                await Shell.Current.GoToAsync("///ProfilePage");
            }
        }
    }

    [RelayCommand]
    private async Task Food() =>
        NotificationHelper.ShowNotification("Gợi Ý Thức Ăn đang phát triển");

    [RelayCommand]
    private async Task Exercise() =>
        NotificationHelper.ShowNotification("Gợi Ý Bài Tập đang phát triển");

    [RelayCommand]
    private async Task Sleep() =>
        NotificationHelper.ShowNotification("Giấc Ngủ đang phát triển");

    [RelayCommand]
    private async Task HealthRecord() =>
        NotificationHelper.ShowNotification("Hồ Sơ Sức Khỏe đang phát triển");

    [RelayCommand]
    private async Task Home() =>
        NotificationHelper.ShowNotification("Bạn đang ở Trang Chủ");

    [RelayCommand]
    private async Task Activity() =>
        NotificationHelper.ShowNotification("Hoạt Động đang phát triển");

    [RelayCommand]
    private async Task Calendar() =>
        NotificationHelper.ShowNotification("Lịch Trình đang phát triển");

    [RelayCommand]
    private async Task Settings() =>
        NotificationHelper.ShowNotification("Cài Đặt đang phát triển");

    #endregion
}