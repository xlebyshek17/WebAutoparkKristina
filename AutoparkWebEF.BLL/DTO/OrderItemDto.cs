using AutoparkWebEF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DetailId { get; set; }
        public int DetailCount { get; set; }

        public SparePartDto Detail { get; set; }
        public OrderDto Order { get; set; }
    }
}
