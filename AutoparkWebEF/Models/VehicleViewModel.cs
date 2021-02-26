using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Models
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Fill in the field Type")]
        [Required]
        public int TypeId { get; set; }

        public string TypeName { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Invalid weight, enter number > 0")]
        [Required]
        public double Weight { get; set; }

        [Range(1885, 2021, ErrorMessage = "Invalid year, enter year > 1885 and < 2021")]
        [Required]
        public int ManufactureYear { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Invalid mileage, enter number > 0")]
        [Required]
        public double Mileage { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Invalid tank volume, enter number > 0")]
        [Required]
        public double TankVolume { get; set; }

        [Required]
        public string Color { get; set; }

        public string Engine { get; set; }

        public double TaxPerMonth { get; set; }
        public double MileageWithFullTank { get; set; }
    }
}
