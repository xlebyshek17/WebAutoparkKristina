using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO.Engine
{
    public class DieselEngine : CombustionEngine
    {
        public DieselEngine(double engineCapacity, double fuelConsumptionPer100) : base("Diesel", 1.2)
        {
            EngineCapacity = engineCapacity;
            FuelConsumptionPer100 = fuelConsumptionPer100;
        }
    }
}
