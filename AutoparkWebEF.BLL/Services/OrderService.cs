using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.BLL.Services
{
    public class OrderService : IService<OrderDto>
    {
        IUnitOfWork db;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork uow, IMapper mapper)
        {
            db = uow;
            _mapper = mapper;
        }

        public void Create(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            var order = _mapper.Map<OrderDto, Order>(orderDto);

            db.Orders.Create(order);
            db.Save();
        }

        public void Delete(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            db.Orders.Delete(orderDto.Id);
            db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<OrderDto> Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Invalid id", "id");

            Order order = await db.Orders.Get(id.Value);

            return _mapper.Map<Order, OrderDto>(order);
        }

        public IEnumerable<OrderDto> GetAll()
        {
            return _mapper.Map<IEnumerable<Order>, List<OrderDto>>(db.Orders.GetAll());
        }

        public void Update(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            Order order = _mapper.Map<OrderDto, Order>(orderDto);

            db.Orders.Update(order);
            db.Save();
        }

    }
}
