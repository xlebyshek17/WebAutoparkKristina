using System;
using System.Collections.Generic;

#nullable disable

namespace AutoparkWebEF.DAL.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
