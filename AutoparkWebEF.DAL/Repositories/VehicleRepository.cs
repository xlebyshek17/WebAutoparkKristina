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

        public override async Task<Vehicle> Get(int id)
        {
            return await base.GetAll().AsQueryable().Include(v => v.Type).FirstOrDefaultAsync(v => v.Id == id);
        }

        public override IEnumerable<Vehicle> GetAll()
        {
            return base.GetAll().AsQueryable().Include(v => v.Type);
        }
    }
}
