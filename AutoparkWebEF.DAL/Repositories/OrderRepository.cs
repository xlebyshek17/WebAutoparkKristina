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
            return await GetAll().FirstOrDefaultAsync(o => o.Id == id);
        }

        public override IQueryable<Order> GetAll()
        {
            return base.GetAll().Include(o => o.Vehicle);
        }

    }
}
