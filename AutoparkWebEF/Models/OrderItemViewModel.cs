using AutoparkWebEF.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Fill in the field OrderId")]
        [Required]
        public int OrderId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Fill in the field Spare Part")]
        [Required]
        public int DetailId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid count, enter number > 0")]
        [Required]
        public int DetailCount { get; set; }
        public string SparePartName { get; set; }
        public string VehicleName { get; set; }
        
    }
}
