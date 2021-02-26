using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO.Engine
{
    public class ElectricalEngine : Engine
    {
        public double ElectricityConsumption { get; set; }

        public ElectricalEngine(double electricityConsumption) : base("Electrical", 0.1)
        {
            ElectricityConsumption = electricityConsumption;
        }

        public override double GetMaxKilometers(double batterySize)
        {
            return batterySize / ElectricityConsumption;
        }
    }
}
