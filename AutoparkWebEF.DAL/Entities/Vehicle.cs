using System;
using System.Collections.Generic;

#nullable disable

namespace AutoparkWebEF.DAL.Entities
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public string ModelName { get; set; }
        public string RegistrationNumber { get; set; }
        public double Weight { get; set; }
        public int ManufactureYear { get; set; }
        public double Mileage { get; set; }
        public double TankVolume { get; set; }
        public string Color { get; set; }
        public string Engine { get; set; }

        public virtual VehicleType Type { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
