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
        private readonly IMapper _mapper;

        public OrderItemViewModelController(IService<OrderItemDto> context, IMapper mapper)
        {
            db = context;
            _mapper = mapper;
        }

        public IActionResult ViewOrderItems()
        {
            var orderItemDtos = db.GetAll();

            var orderItems = _mapper.Map<IEnumerable<OrderItemDto>, List<OrderItemViewModel>>(orderItemDtos);
            return View(orderItems);
        }

        public async Task<IActionResult> OrderItemDetails(int id)
        {
            var orderItemDto = await db.Get(id);
            var orderItem = _mapper.Map<OrderItemDto, OrderItemViewModel>(orderItemDto);

            return View(orderItem) ?? (IActionResult)NotFound();
        }

        public IActionResult CreateOrderItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrderItem(OrderItemViewModel orderItem)
        {
            var orderItemDto = _mapper.Map<OrderItemViewModel, OrderItemDto>(orderItem);
            db.Create(orderItemDto);
            return RedirectToAction("ViewOrderItems");
        }
    }
}
