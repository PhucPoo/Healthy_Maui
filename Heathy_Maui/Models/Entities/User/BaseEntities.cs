namespace HealthManagement_MAUI.Models.Entities
{
    public class BaseEntities
    {
        public int Id { get; set; }
        public bool isDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
