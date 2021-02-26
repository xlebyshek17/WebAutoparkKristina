using AutoparkWebEF.BLL.DTO.Engine;
using AutoparkWebEF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO
{
    public class VehicleDto
    {
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
        public VehicleTypeDto Type { get; set; }

        public double TaxPerMonth
        {
            get
            {
                return (Weight * 0.0013) + (GetEngineByName(Enum.Parse<EngineNames>(Engine)).EngineTaxCoefficient * Type.TaxCoefficient * 30) + 5;
            }
        }

        public double MileageWithFullTank
        {
            get
            {
                return GetEngineByName(Enum.Parse<EngineNames>(Engine)).GetMaxKilometers(TankVolume);
            }
        }

        private Engine.Engine GetEngineByName(EngineNames name)
        {
            switch (name)
            {
                case EngineNames.Gasoline:
                    return new GasolineEngine(2.1, 8.3);
                case EngineNames.Diesel:
                    return new DieselEngine(3, 15);
                case EngineNames.Electrical:
                    return new ElectricalEngine(40);
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
