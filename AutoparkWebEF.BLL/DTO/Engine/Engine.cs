using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO.Engine
{
    public abstract class Engine
    {
        public string EngineType { get; set; }
        public double EngineTaxCoefficient { get; set; }

        public Engine(string engineType, double engineTaxCoefficient)
        {
            EngineType = engineType;
            EngineTaxCoefficient = engineTaxCoefficient;
        }

        public abstract double GetMaxKilometers(double fuelTankCapacity);
    }
}
