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
        private readonly IMapper _mapper;

        public OrderViewModelController(IService<OrderDto> context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public IActionResult ViewOrders()
        {
            var orderDtos = db.GetAll();
            var orders = _mapper.Map<IEnumerable<OrderDto>, List<OrderViewModel>>(orderDtos);
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var orderDto = await db.Get(id);
            var order = _mapper.Map<OrderDto, OrderViewModel>(orderDto);

            return View(order) ?? (IActionResult)NotFound();
        }

        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderViewModel order)
        {
            var orderDto = _mapper.Map<OrderViewModel, OrderDto>(order);
            db.Create(orderDto);
            return RedirectToAction("ViewOrders");
        }
    }
}
