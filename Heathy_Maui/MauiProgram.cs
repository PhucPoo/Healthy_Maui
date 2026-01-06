using CommunityToolkit.Maui;
using HealthManagement_MAUI.Data.Repositories;
using Healthy_MAUI.ViewModels;
using Healthy_MAUI.Views.Auth;
using Healthy_MAUI.Views.Home;
using Heathy_Maui.Data;
using Heathy_Maui.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Heathy_Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Đăng kí mainPage
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterPageViewModel>();
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // SQL Server connection
            var connectionString = "Server=DESKTOP-T45MDSR\\SQLEXPRESS;Database=Healthy;Trusted_Connection=True;TrustServerCertificate=True;";
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated(); // <- tạo bảng + seed dữ liệu
            }

            return app;

        }
    }
}
