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
    public class VehicleTypeRepository : GenericRepository<VehicleType>
    {

        public VehicleTypeRepository(AutoparkContext context) : base(context)
        {

        }
    }
}
