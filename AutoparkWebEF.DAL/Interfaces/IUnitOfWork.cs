using AutoparkWebEF.DAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AutoparkWebEF.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<VehicleType> VehicleTypes { get; }
        IRepository<Vehicle> Vehicles { get; }
        IRepository<SparePart> SpareParts { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderItem> OrderItems { get; }
        void Save();
    }
}
