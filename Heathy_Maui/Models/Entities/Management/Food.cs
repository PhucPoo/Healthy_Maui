using HealthManagement_MAUI.Models.Entities.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthManagement_MAUI.Models.Entities.Management
{
    public class Food : BaseEntities
    {
        public int ProfileId { get; set; }
        public AccountProfile AccountProfile { get; set; } = null!;

        public string FoodName { get; set; } = null!;
        public string? Description { get; set; }
        public int Calories { get; set; }
        public string? MealType { get; set; }
        public DateTime DateConsumed { get; set; }
    }
}
