﻿using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Infastructure;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.BLL.Services
{
    public class OrderItemService : IService<OrderItemDto>
    {
        IUnitOfWork db;

        public OrderItemService(IUnitOfWork uow)
        {
            db = uow;
        }
        public void Create(OrderItemDto orderItemDto)
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

        public async Task<IEnumerable<OrderItemDto>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderItem, OrderItemDto>()).CreateMapper();
            return mapper.Map<IEnumerable<OrderItem>, List<OrderItemDto>>(await db.OrderItems.GetAll());
        }

        public void Update(OrderItemDto orderItemDto)
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
            db.Save();
        }
    }
}
