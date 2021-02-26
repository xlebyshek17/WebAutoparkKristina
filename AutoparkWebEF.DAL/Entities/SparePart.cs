using System;
using System.Collections.Generic;

#nullable disable

namespace AutoparkWebEF.DAL.Entities
{
    public partial class SparePart
    {
        public SparePart()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
