using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Fill in the field Vehicle")]
        [Required]
        public int VehicleId { get; set; }
        public string VehicleName { get; set; }
    }
}
