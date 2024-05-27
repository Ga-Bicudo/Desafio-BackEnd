using System.ComponentModel.DataAnnotations;

namespace MotorcyleRental.Models
{
    public class MotorCycleViewModel
    {
        public DateOnly CreationDate { get; set; }
        public DateOnly MaintenanceDate { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Plate { get; set; }

        [Required]
        public string FactoryYear { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
