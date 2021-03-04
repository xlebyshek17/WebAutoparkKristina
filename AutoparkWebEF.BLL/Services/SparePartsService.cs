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
    public class SparePartsService : IService<SparePartDto>
    {
        IUnitOfWork db;

        public SparePartsService(IUnitOfWork uow)
        {
            db = uow;
        }

        public async Task Create(SparePartDto sparePartDTO)
        {
            if (sparePartDTO == null)
                throw new ValidationException("Spare part not found", "");

            SparePart sparePart = new SparePart
            {
                Id = sparePartDTO.Id,
                Name = sparePartDTO.Name
            };

            await db.SpareParts.Create(sparePart);
            await db.Save();
        }

        public async Task Delete(SparePartDto sparePartDTO)
        {
            if (sparePartDTO == null)
                throw new ValidationException("Spare part not found", "");

            await db.SpareParts.Delete(sparePartDTO.Id);
            await db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<SparePartDto> Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Invalid id", "id");

            SparePart sparePart = await db.SpareParts.Get(id.Value);

            if (sparePart == null)
                throw new ValidationException("Spare part not found", "");

            return new SparePartDto { Id = sparePart.Id, Name = sparePart.Name };
        }

        public IQueryable<SparePartDto> GetAll()
        {
            var conf = new MapperConfiguration(cfg => cfg.CreateMap<SparePart, SparePartDto>());
            return db.SpareParts.GetAll().ProjectTo<SparePartDto>(conf);
        }

        public async Task Update(SparePartDto sparePartDTO)
        {
            if (sparePartDTO == null)
                throw new ValidationException("Spare part not found", "");

            SparePart sparePart = new SparePart
            {
                Id = sparePartDTO.Id,
                Name = sparePartDTO.Name
            };

            db.SpareParts.Update(sparePart);
            await db.Save();
        }
    }
}
