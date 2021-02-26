using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Models
{
    public class VehicleTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string TypeName { get; set; }

        [Range(0, 1000, ErrorMessage = "Invalid tax coefficient (enter number >0)")]
        [Required]
        public double TaxCoefficient { get; set; }
    }
}
