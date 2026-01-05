using HealthManagement_MAUI.Models.Entities.Profile;
namespace HealthManagement_MAUI.Models.Entities.Management
{
    public class Sleep : BaseEntities
    {
        public int ProfileId { get; set; }
        public AccountProfile AccountProfile { get; set; } = null!;

        public DateTime SleepDate { get; set; }
        public DateTime SleepStartTime { get; set; }
        public DateTime SleepEndTime { get; set; }
        public int SleepQuality { get; set; }
        public string? Notes { get; set; }
    }
}
