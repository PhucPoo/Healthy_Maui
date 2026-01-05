using HealthManagement_MAUI.Models.Entities.Profile;
using System.Data;

namespace HealthManagement_MAUI.Models.Entities
{
    public class Account : BaseEntities
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public AccountProfile AccountProfile { get; set; } = null!;
    }

}
