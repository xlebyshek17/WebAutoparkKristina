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
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace AutoparkWebEF.BLL.Services
{
    public class OrderService : IService<OrderDto>
    {
        IUnitOfWork db;

        public OrderService(IUnitOfWork uow)
        {
            db = uow;
        }

        public async Task Create(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            Order order = new Order
            {
                Id = orderDto.Id,
                VehicleId = orderDto.VehicleId
            };

            await db.Orders.Create(order);
            await db.Save();
        }

        public async Task Delete(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            await db.Orders.Delete(orderDto.Id);
            await db.Save();
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

        public IQueryable<OrderDto> GetAll()
        {
            var conf = new MapperConfiguration(cfg => { cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<Vehicle, VehicleDto>();
            });
            return db.Orders.GetAll().ProjectTo<OrderDto>(conf);
        }

        public async Task Update(OrderDto orderDto)
        {
            if (orderDto == null)
                throw new ValidationException("Order not found", "");

            Order order = new Order
            {
                Id = orderDto.Id,
                VehicleId = orderDto.VehicleId
            };

            db.Orders.Update(order);
            await db.Save();
        }

    }
}
