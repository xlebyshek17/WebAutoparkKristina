using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO.Engine
{
    public abstract class CombustionEngine : Engine
    {
        public double FuelConsumptionPer100 { get; set; }
        public double EngineCapacity { get; set; }

        public CombustionEngine(string typeName, double engineTaxCoefficient) : base(typeName, engineTaxCoefficient)
        {

        }

        public override double GetMaxKilometers(double fuelTankCapacity)
        {
            return fuelTankCapacity * 100 / FuelConsumptionPer100;
        }
    }
}
