using System;
using System.Collections.Generic;

#nullable disable

namespace AutoparkWebEF.DAL.Entities
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public double TaxCoefficient { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
