using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO
{
    public class VehicleTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public double TaxCoefficient { get; set; }
    }
}
