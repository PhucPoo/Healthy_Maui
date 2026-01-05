using HealthManagement_MAUI.Models.Entities.Profile;

namespace HealthManagement_MAUI.Models.Entities.Management
{
    public class Exercise : BaseEntities
    {
        public int ProfileId { get; set; }
        public AccountProfile AccountProfile { get; set; } = null!;

        public string ExerciseName { get; set; } = null!;
        public string? Description { get; set; }
        public int DurationMinutes { get; set; }
        public int CaloriesBurned { get; set; }
        public DateTime DatePerformed { get; set; }
    }
}
