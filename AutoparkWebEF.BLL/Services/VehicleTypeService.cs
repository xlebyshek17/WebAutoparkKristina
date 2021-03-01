using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(VehicleTypeDto typeDto)
        {
            if (typeDto == null)
                throw new ValidationException("Vehicle type not found", "");

            VehicleType type = new VehicleType
            {
                Id = typeDto.Id,
                TypeName = typeDto.TypeName,
                TaxCoefficient = typeDto.TaxCoefficient
            };

            db.VehicleTypes.Create(type);
            db.Save();
        }

        public void Delete(VehicleTypeDto typeDto)
        {
            if (typeDto == null)
                throw new ValidationException("Vehicle type not found", "");

            db.VehicleTypes.Delete(typeDto.Id);
            db.Save();
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

        public IEnumerable<VehicleTypeDto> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => { cfg.CreateMap<VehicleType, VehicleTypeDto>();
                cfg.CreateMap<VehicleType, VehicleTypeDto>();
            }).CreateMapper();
            return mapper.Map<IEnumerable<VehicleType>, List<VehicleTypeDto>>(db.VehicleTypes.GetAll());
        }

        public void Update(VehicleTypeDto typeDto)
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
            db.Save();
        }
    }
}
