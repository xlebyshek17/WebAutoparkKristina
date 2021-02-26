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
    public class VehicleTypeRepository : IRepository<VehicleType>
    {
        private AutoparkContext db;

        public VehicleTypeRepository(AutoparkContext context)
        {
            db = context;
        }

        public async void Create(VehicleType type)
        {
            await db.VehicleTypes.AddAsync(type);
        }

        public async void Delete(int id)
        {
            var type = await db.VehicleTypes.FindAsync(id);
            if (type != null)
                db.VehicleTypes.Remove(type);
        }

        public async Task<VehicleType> Get(int id)
        {
            return await db.VehicleTypes.FindAsync(id);
        }

        public async Task<IEnumerable<VehicleType>> GetAll()
        {
            return await db.VehicleTypes.ToListAsync();
        }

        public void Update(VehicleType type)
        {
            db.Entry(type).State = EntityState.Modified;
        }
    }
}
