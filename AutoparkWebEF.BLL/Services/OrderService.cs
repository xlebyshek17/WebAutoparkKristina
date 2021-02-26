using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.BLL.Services
{
    public class OrderService : IService<OrderDto>
    {
        IUnitOfWork db;

        public OrderService(IUnitOfWork uow)
        {
            db = uow;
        }

        public void Create(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            Order order = new Order
            {
                Id = orderDto.Id,
                VehicleId = orderDto.VehicleId
            };

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

            return new OrderDto { Id = order.Id, VehicleId = order.VehicleId };
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => { cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Vehicle, VehicleDto>();
            }).CreateMapper();
            return mapper.Map<IEnumerable<Order>, List<OrderDto>>(await db.Orders.GetAll());
        }

        public void Update(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            Order order = new Order
            {
                Id = orderDto.Id,
                VehicleId = orderDto.VehicleId
            };

            db.Orders.Update(order);
            db.Save();
        }

    }
}
