using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.BLL.Services
{
    public class VehicleTypeService : IService<VehicleTypeDto>
    {
        IUnitOfWork db;

        public VehicleTypeService(IUnitOfWork uow)
        {
            db = uow;
        }

        public async Task Create(VehicleTypeDto typeDto)
        {
            if (typeDto == null)
                throw new ValidationException("Vehicle type not found", "");

            VehicleType type = new VehicleType
            {
                Id = typeDto.Id,
                TypeName = typeDto.TypeName,
                TaxCoefficient = typeDto.TaxCoefficient
            };

            await db.VehicleTypes.Create(type);
            await db.Save();
        }

        public async Task Delete(VehicleTypeDto typeDto)
        {
            if (typeDto == null)
                throw new ValidationException("Vehicle type not found", "");

            await db.VehicleTypes.Delete(typeDto.Id);
            await db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<VehicleTypeDto> Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Invalid id", "id");

            VehicleType type = await db.VehicleTypes.Get(id.Value);
            if (type == null)
                throw new ValidationException("Invalid vehicle type", "");

            return new VehicleTypeDto { Id = type.Id, TypeName = type.TypeName, TaxCoefficient = type.TaxCoefficient };
        }

        public IQueryable<VehicleTypeDto> GetAll()
        {
            var conf = new MapperConfiguration(cfg => cfg.CreateMap<VehicleType, VehicleTypeDto>());
            return db.VehicleTypes.GetAll().ProjectTo<VehicleTypeDto>(conf);
        }

        public async Task Update(VehicleTypeDto typeDto)
        {
            if (typeDto == null)
                throw new ValidationException("Vehicle type not found", "");

            VehicleType type = new VehicleType
            {
                Id = typeDto.Id,
                TypeName = typeDto.TypeName,
                TaxCoefficient = typeDto.TaxCoefficient
            };

            db.VehicleTypes.Update(type);
            await db.Save();
        }
    }
}
