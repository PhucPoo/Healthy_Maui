namespace HealthManagement_MAUI.Models.Entities
{
    public class Role : BaseEntities
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
