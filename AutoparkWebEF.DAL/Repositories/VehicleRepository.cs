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
    public class VehicleRepository : IRepository<Vehicle>
    {
        private AutoparkContext db;

        public VehicleRepository(AutoparkContext context)
        {
            db = context;
        }
        
        public async void Create(Vehicle vehicle)
        {
            await db.Vehicles.AddAsync(vehicle);
        }

        public async void Delete(int id)
        {
            var vehicle = await db.Vehicles.FindAsync(id);
            if (vehicle != null)
                db.Vehicles.Remove(vehicle);
        }

        public async Task<Vehicle> Get(int id)
        {
            return await db.Vehicles.Include(v => v.Type).Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            return await db.Vehicles.Include(v => v.Type).ToListAsync();
        }

        public void Update(Vehicle vehicle)
        {
            db.Vehicles.Update(vehicle);
            //db.Entry(vehicle).State = EntityState.Modified;
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
