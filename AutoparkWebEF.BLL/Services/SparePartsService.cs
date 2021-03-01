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
    public class SparePartsService : IService<SparePartDto>
    {
        IUnitOfWork db;
        private readonly IMapper _mapper;

        public SparePartsService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            _mapper = mapper;
        }

        public void Create(SparePartDto sparePartDTO)
        {
            if (sparePartDTO == null)
                throw new ValidationException("Spare part not found", "");

            var sparePart = _mapper.Map<SparePartDto, SparePart>(sparePartDTO);

            db.SpareParts.Create(sparePart);
            db.Save();
        }

        public void Delete(SparePartDto sparePartDTO)
        {
            if (sparePartDTO == null)
                throw new ValidationException("Spare part not found", "");

            db.SpareParts.Delete(sparePartDTO.Id);
            db.Save();
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

            return _mapper.Map<SparePart, SparePartDto>(sparePart);
        }

        public IEnumerable<SparePartDto> GetAll()
        {
            return _mapper.Map<IEnumerable<SparePart>, List<SparePartDto>>(db.SpareParts.GetAll());
        }

        public void Update(SparePartDto sparePartDTO)
        {
            if (sparePartDTO == null)
                throw new ValidationException("Spare part not found", "");

            var sparePart = _mapper.Map<SparePartDto, SparePart>(sparePartDTO);

            db.SpareParts.Update(sparePart);
            db.Save();
        }
    }
}
