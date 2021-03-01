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
    public class OrderViewModelController : Controller
    {
        IService<OrderDto> db;

        public OrderViewModelController(IService<OrderDto> context)
        {
            db = context;
        }

        public IActionResult ViewOrders()
        {
            var orderDtos = db.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDto, OrderViewModel>().ForMember(dest => dest.VehicleName, act => act.MapFrom(src => src.Vehicle.ModelName))).CreateMapper();
            var orders = mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(orderDtos);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var orderDto = await db.Get(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDto, OrderViewModel>().ForMember(dest => dest.VehicleName, act => act.MapFrom(src => src.Vehicle.ModelName))).CreateMapper();
            var order = mapper.Map<OrderDto, OrderViewModel>(orderDto);

            return View(order) ?? (IActionResult)NotFound();
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel order)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderViewModel, OrderDto>()).CreateMapper();
            var orderDto = mapper.Map<OrderViewModel, OrderDto>(order);
            db.Create(orderDto);
            return RedirectToAction("ViewOrders");
        }
    }
}
