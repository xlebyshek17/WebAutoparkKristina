using AutoparkWebEF.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.BLL.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
