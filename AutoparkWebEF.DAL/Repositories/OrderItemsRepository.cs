using AutoparkWebEF.DAL.EF;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Repositories
{
    // TODO: You can generalize all repository actions in one abstract class. Read about generic repository and make it up for all your repos.
    public class OrderItemsRepository : GenericRepository<OrderItem>
    {
        public OrderItemsRepository(AutoparkContext context) : base(context)
        {
        }

        public override async Task<OrderItem> Get(int id)
        {
            return await base.GetAll().AsQueryable().Include(o => o.Detail).Include(o => o.Order).ThenInclude(o => o.Vehicle).FirstOrDefaultAsync(ord => ord.Id == id);
            
        }

        public override IEnumerable<OrderItem> GetAll()
        {
            return base.GetAll().AsQueryable().Include(o => o.Order).Include(o => o.Detail);
        }
    }
}
