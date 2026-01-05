using HealthManagement_MAUI.Models.Entities.Profile;

namespace HealthManagement_MAUI.Models.Entities.Management
{
    public class Health : BaseEntities
    {
        public int ProfileId { get; set; }
        public AccountProfile AccountProfile { get; set; } = null!;

        public DateTime Date { get; set; }
        public int CaloriesConsumed { get; set; }
        public int CaloriesBurned { get; set; }
        public int StepsCount { get; set; }
        public float WaterIntakeLiters { get; set; }
        public float SleepHours { get; set; }
        public string? Notes { get; set; }
    }
}
