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
    public class OrderItemsRepository : IRepository<OrderItem>
    {
        private AutoparkContext db;

        public OrderItemsRepository(AutoparkContext context)
        {
            db = context;
        }

        public async void Create(OrderItem orderItem)
        {
            await db.OrderItems.AddAsync(orderItem);
        }

        public async void Delete(int id)
        {
            var orderItem = await db.OrderItems.FindAsync(id);
            if (orderItem != null)
                db.OrderItems.Remove(orderItem);
        }

        public async Task<OrderItem> Get(int id)
        {
            return await db.OrderItems.Include(o => o.Detail).Include(o => o.Order).ThenInclude(o => o.Vehicle).Where(ord => ord.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            return await db.OrderItems.Include(o => o.Order).Include(o => o.Detail).ToListAsync();
        }

        public void Update(OrderItem orderItem)
        {
            db.Entry(orderItem).State = EntityState.Modified;
        }
    }
}
