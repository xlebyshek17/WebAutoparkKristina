using AutoparkWebEF.DAL.EF;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Repositories
{
    public class SparePartRepository : IRepository<SparePart>
    {
        private AutoparkContext db;

        public SparePartRepository(AutoparkContext context)
        {
            db = context;
        }

        public async void Create(SparePart sparePart)
        {
            await db.SpareParts.AddAsync(sparePart);
        }

        public async void Delete(int id)
        {
            var sparePart = await db.SpareParts.FindAsync(id);
            if (sparePart != null)
                db.SpareParts.Remove(sparePart);
        }

        public async Task<SparePart> Get(int id)
        {
            return await db.SpareParts.FindAsync(id);
        }

        public async Task<IEnumerable<SparePart>> GetAll()
        {
            return await db.SpareParts.ToListAsync();
        }

        public void Update(SparePart sparePart)
        {
            db.Entry(sparePart).State = EntityState.Modified;
        }
    }
}
