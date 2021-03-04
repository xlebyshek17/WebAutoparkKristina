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
    public class VehicleRepository : GenericRepository<Vehicle>
    {
        public VehicleRepository(AutoparkContext context) : base(context)
        {

        }

        public async override Task<Vehicle> Get(int id)
        {
            return await GetAll().FirstOrDefaultAsync(v => v.Id == id);
        }

        public override IQueryable<Vehicle> GetAll()
        {
            return base.GetAll().Include(v => v.Type);
        }
    }
}
