using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace AutoparkWebEF.BLL.Services
{
    public class VehicleService : IService<VehicleDto>
    {
        IUnitOfWork db { get; set; }

        public VehicleService(IUnitOfWork uow)
        {
            db = uow;
        }

        public void Create(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                throw new ValidationException("Vehicle not found", "");

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleDto, Vehicle>();
            }).CreateMapper();
            var vehicle = mapper.Map<VehicleDto, Vehicle>(vehicleDto);

            db.Vehicles.Create(vehicle);
            db.Save();
        }

        public void Delete(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                throw new ValidationException("Vehicle not found", "");

            db.Vehicles.Delete(vehicleDto.Id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<VehicleDto> Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Invalid id", "id");

            Vehicle vehicle = await db.Vehicles.Get(id.Value);

            if (vehicle == null)
                throw new ValidationException("Vehicle not found", "");

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleType, VehicleTypeDto>();
            }).CreateMapper();
            return mapper.Map<Vehicle, VehicleDto>(vehicle);
        }

        public IEnumerable<VehicleDto> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleType, VehicleTypeDto>();
            }).CreateMapper();
            return mapper.Map<IEnumerable<Vehicle>, List<VehicleDto>>(db.Vehicles.GetAll());
        }

        public void Update(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                throw new ValidationException("Vehicle not found", "");

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleDto, Vehicle>();
            }).CreateMapper();
            var vehicle = mapper.Map<VehicleDto, Vehicle>(vehicleDto);

            db.Vehicles.Update(vehicle);
            db.Save();
        }
    }
}
