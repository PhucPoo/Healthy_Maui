using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthManagement_MAUI.Models.Entities.Profile
{
    public class HealthProfile : BaseEntities
    {
        public int ProfileId { get; set; }
        public AccountProfile AccountProfile { get; set; } = null!;

        public float HeightCm { get; set; }
        public float WeightKg { get; set; }
        public string? BloodType { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Allergies { get; set; }
    }
}
