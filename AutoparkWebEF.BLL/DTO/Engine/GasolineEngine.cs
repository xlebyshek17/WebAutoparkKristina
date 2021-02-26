using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO.Engine
{
    public class GasolineEngine : CombustionEngine
    {
        public GasolineEngine(double engineCapacity, double fuelConsumptionPer100) : base("Gasoline", 1)
        {
            EngineCapacity = engineCapacity;
            FuelConsumptionPer100 = fuelConsumptionPer100;
        }
    }
}
