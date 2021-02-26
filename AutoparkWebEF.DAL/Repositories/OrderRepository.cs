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
    public class OrderRepository : IRepository<Order>
    {
        private AutoparkContext db;

        public OrderRepository(AutoparkContext context)
        {
            db = context;
        }

        public async void Create(Order order)
        {
            await db.Orders.AddAsync(order);
        }

        public async void Delete(int id)
        {
            var order = await db.Orders.FindAsync(id);
            if (order != null)
                db.Orders.Remove(order);
        }

        public async Task<Order> Get(int id)
        {
            return await db.Orders.Include(o => o.Vehicle).Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await db.Orders.Include(o => o.Vehicle).ToListAsync();
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }
    }
}
