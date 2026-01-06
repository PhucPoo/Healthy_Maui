namespace Healthy_MAUI.Models.Entities.Profile
{
    /// <summary>
    /// Message được gửi khi đăng nhập thành công
    /// </summary>
    public class LoginMessage
    {
    }

    /// <summary>
    /// Message được gửi khi đăng xuất
    /// </summary>
    public class LogoutMessage
    {
    }

    /// <summary>
    /// Message được gửi khi cập nhật profile
    /// </summary>
    public class ProfileUpdatedMessage
    {
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
    }
}