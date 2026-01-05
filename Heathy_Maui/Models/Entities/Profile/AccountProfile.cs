using HealthManagement_MAUI.Models.Entities.Management;

namespace HealthManagement_MAUI.Models.Entities.Profile
{
    public class AccountProfile : BaseEntities
    {
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public HealthProfile HealthProfile { get; set; } = null!;
        public ICollection<Health> HealthManagements { get; set; } = new List<Health>();
        public ICollection<Food> FoodManagements { get; set; } = new List<Food>();
        public ICollection<Exercise> ExerciseManagements { get; set; } = new List<Exercise>();
        public ICollection<Sleep> SleepManagements { get; set; } = new List<Sleep>();

    }
}
