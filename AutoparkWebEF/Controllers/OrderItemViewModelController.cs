using AutoMapper;
using AutoparkWebEF.BLL.DTO;
using AutoparkWebEF.BLL.Interfaces;
using AutoparkWebEF.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoparkWebEF.Controllers
{
    public class OrderItemViewModelController : Controller
    {
        IService<OrderItemDto> db;

        public OrderItemViewModelController(IService<OrderItemDto> context)
        {
            db = context;
        }

        public async Task<IActionResult> ViewOrderItems()
        {
            var orderItemDtos = await db.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemDto, OrderItemViewModel>().ForMember(dest => dest.SparePartName, act => act.MapFrom(src => src.Detail.Name))).CreateMapper();
            var orderItems = mapper.Map<IEnumerable<OrderItemDto>, List<OrderItemViewModel>>(orderItemDtos);
            return View(orderItems);
        }

        public async Task<IActionResult> OrderItemDetails(int id)
        {
            var orderItemDto = await db.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemDto, OrderItemViewModel>().ForMember(dest => dest.SparePartName, act => act.MapFrom(src => src.Detail.Name))
            .ForMember(dest => dest.VehicleName, act => act.MapFrom(src => src.Order.Vehicle.ModelName))).CreateMapper();
            var orderItem = mapper.Map<OrderItemDto, OrderItemViewModel>(orderItemDto);

            return View(orderItem) ?? (IActionResult)NotFound();
        }

        public IActionResult CreateOrderItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrderItem(OrderItemViewModel orderItem)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemViewModel, OrderItemDto>()).CreateMapper();
            var orderItemDto = mapper.Map<OrderItemViewModel, OrderItemDto>(orderItem);
            db.Create(orderItemDto);
            return RedirectToAction("ViewOrderItems");
        }
    }
}
