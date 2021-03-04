using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.BLL.Services
{
    public class VehicleService : IService<VehicleDto>
    {
        IUnitOfWork db { get; set; }

        public VehicleService(IUnitOfWork uow)
        {
            db = uow;
        }

        public async Task Create(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                throw new ValidationException("Vehicle not found", "");

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleDto, Vehicle>();
            }).CreateMapper();
            var vehicle = mapper.Map<VehicleDto, Vehicle>(vehicleDto);

            await db.Vehicles.Create(vehicle);
            await db.Save();
        }

        public async Task Delete(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                throw new ValidationException("Vehicle not found", "");

            await db.Vehicles.Delete(vehicleDto.Id);
            await db.Save();
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

        public IQueryable<VehicleDto> GetAll()
        {
            var conf = new MapperConfiguration(cfg => { cfg.CreateMap<Vehicle, VehicleDto>();
                cfg.CreateMap<VehicleType, VehicleTypeDto>();
            });
            return db.Vehicles.GetAll().ProjectTo<VehicleDto>(conf);
        }

        public async Task Update(VehicleDto vehicleDto)
        {
            if (vehicleDto == null)
                throw new ValidationException("Vehicle not found", "");

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleDto, Vehicle>();
            }).CreateMapper();
            var vehicle = mapper.Map<VehicleDto, Vehicle>(vehicleDto);

            db.Vehicles.Update(vehicle);
            await db.Save();
        }
    }
}
