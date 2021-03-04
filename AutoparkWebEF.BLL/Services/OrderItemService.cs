using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace AutoparkWebEF.BLL.Services
{
    public class OrderItemService : IService<OrderItemDto>
    {
        IUnitOfWork db;

        public OrderItemService(IUnitOfWork uow)
        {
            db = uow;
        }
        public async Task Create(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ValidationException("Order item not found", "");

            OrderItem orderItem = new OrderItem
            {
                Id = orderItemDto.Id,
                OrderId = orderItemDto.OrderId,
                DetailId = orderItemDto.DetailId,
                DetailCount = orderItemDto.DetailCount
            };

            await db.OrderItems.Create(orderItem);
            await db.Save();
        }

        public async Task Delete(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ValidationException("Order item not found", "");

            await db.OrderItems.Delete(orderItemDto.Id);
            await db.Save();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public async Task<OrderItemDto> Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Invalid id", "id");

            OrderItem orderItem = await db.OrderItems.Get(id.Value);

            if (orderItem == null)
                throw new ValidationException("Order item not found", "");

            return new OrderItemDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                DetailId = orderItem.DetailId,
                DetailCount = orderItem.DetailCount,
                Order = orderItem.Order,
                Detail = orderItem.Detail
            };
        }

        public IQueryable<OrderItemDto> GetAll()
        {
            var conf = new MapperConfiguration(cfg => cfg.CreateMap<OrderItem, OrderItemDto>());
            return db.OrderItems.GetAll().ProjectTo<OrderItemDto>(conf);
        }

        public async Task Update(OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
                throw new ValidationException("Order item not found", "");

            OrderItem orderItem = new OrderItem
            {
                Id = orderItemDto.Id,
                OrderId = orderItemDto.OrderId,
                DetailId = orderItemDto.DetailId,
                DetailCount = orderItemDto.DetailCount
            };

            db.OrderItems.Update(orderItem);
            await db.Save();
        }
    }
}
