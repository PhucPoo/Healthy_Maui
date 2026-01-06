using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heathy_Maui.Helpers
{
    public static class NotificationHelper
    {
        public static void ShowNotification(string message)
        {
            // Đảm bảo chạy trên UI thread
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                // Nếu Toast hỗ trợ nền tảng hiện tại
                if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    await Toast.Make(message, ToastDuration.Short, 14).Show();
                }
                else
                {
                    // Windows hoặc các nền tảng khác, dùng DisplayAlert
                    await App.Current.MainPage.DisplayAlert("Thông báo", message, "OK");
                }
            });
        }
    }
}
