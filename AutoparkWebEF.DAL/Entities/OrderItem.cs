using System;
using System.Collections.Generic;

#nullable disable

namespace AutoparkWebEF.DAL.Entities
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DetailId { get; set; }
        public int DetailCount { get; set; }

        public virtual SparePart Detail { get; set; }
        public virtual Order Order { get; set; }
    }
}
