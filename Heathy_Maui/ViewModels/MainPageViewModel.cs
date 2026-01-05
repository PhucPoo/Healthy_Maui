using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
    private string avatarText = "A";
    #endregion

    public MainPageViewModel()
    {
    }

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
    private async Task ShowToast(string message)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var toast = Toast.Make(message, ToastDuration.Short);
        await toast.Show(cancellationTokenSource.Token);
    }
    #endregion

    #region Commands
    [RelayCommand]
    private async Task Login()
    {
        IsLoggedIn = !IsLoggedIn;
        IsLoginButtonVisible = !IsLoggedIn;
        IsAvatarVisible = IsLoggedIn;

        await ShowToast(IsLoggedIn ? "Đã đăng nhập" : "Đã đăng xuất");
    }

    [RelayCommand]
    private async Task Food() =>
        await ShowToast("Gợi Ý Thức Ăn đang phát triển");

    [RelayCommand]
    private async Task Exercise() =>
        await ShowToast("Gợi Ý Bài Tập đang phát triển");

    [RelayCommand]
    private async Task Sleep() =>
        await ShowToast("Giấc Ngủ đang phát triển");

    [RelayCommand]
    private async Task HealthRecord() =>
        await ShowToast("Hồ Sơ Sức Khỏe đang phát triển");

    [RelayCommand]
    private async Task Home() =>
        await ShowToast("Bạn đang ở Trang Chủ");

    [RelayCommand]
    private async Task Activity() =>
        await ShowToast("Hoạt Động đang phát triển");

    [RelayCommand]
    private async Task Calendar() =>
        await ShowToast("Lịch Trình đang phát triển");

    [RelayCommand]
    private async Task Settings() =>
        await ShowToast("Cài Đặt đang phát triển");

    #endregion
}
