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
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(AutoparkContext context) : base(context)
        {

        }


        public override async Task<Order> Get(int id)
        {
            return await base.GetAll().AsQueryable().Include(o => o.Vehicle).FirstOrDefaultAsync(o => o.Id == id);
        }

        public override IEnumerable<Order> GetAll()
        {
            return base.GetAll().AsQueryable().Include(o => o.Vehicle);
        }
    }
}
