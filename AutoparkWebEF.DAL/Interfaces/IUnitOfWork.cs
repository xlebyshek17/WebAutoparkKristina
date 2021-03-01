using AutoparkWebEF.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<VehicleType> VehicleTypes { get; }
        IGenericRepository<Vehicle> Vehicles { get; }
        IGenericRepository<SparePart> SpareParts { get; }
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderItem> OrderItems { get; }
        void Save();
    }
}
