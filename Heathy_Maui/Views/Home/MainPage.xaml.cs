
using Healthy_MAUI.ViewModels;
namespace Healthy_MAUI.Views.Home;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        _viewModel = new MainPageViewModel();
        BindingContext = _viewModel;

        _viewModel.StartBannerAnimation(this.Dispatcher);
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.StopBannerAnimation();
    }
}