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
    public class OrderItemService : IService<OrderItemDto>
    {
        IUnitOfWork db;
        private readonly IMapper _mapper;

        public OrderItemService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            _mapper = mapper;
        }
        public void Create(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ValidationException("Order item not found", "");

            var orderItem = _mapper.Map<OrderItemDto, OrderItem>(orderItemDto);

            db.OrderItems.Create(orderItem);
            db.Save();
        }

        public void Delete(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ValidationException("Order item not found", "");

            db.OrderItems.Delete(orderItemDto.Id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<OrderItemDto> Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Invalid id", "id");

            var orderItem = await db.OrderItems.Get(id.Value);

            if (orderItem == null)
                throw new ValidationException("Order item not found", "");

            return _mapper.Map<OrderItem, OrderItemDto>(orderItem);
        }

        public IEnumerable<OrderItemDto> GetAll()
        {
            return _mapper.Map<IEnumerable<OrderItem>, List<OrderItemDto>>(db.OrderItems.GetAll());
        }

        public void Update(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ValidationException("Order item not found", "");

            var orderItem = _mapper.Map<OrderItemDto, OrderItem>(orderItemDto);

            db.OrderItems.Update(orderItem);
            db.Save();
        }
    }
}
