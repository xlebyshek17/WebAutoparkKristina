using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Models
{
    public class SparePartViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
