using AutoparkWebEF.DAL.EF;
using AutoparkWebEF.DAL.Entities;
using AutoparkWebEF.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AutoparkWebEF.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AutoparkContext db;
        private VehicleRepository vehicleRepo;
        private VehicleTypeRepository vehicleTypeRepo;
        private SparePartRepository sparePartRepo;
        private OrderRepository orderRepo;
        private OrderItemsRepository orderItemsRepo;

        public EFUnitOfWork(DbContextOptions<AutoparkContext> connection)
        {
            db = new AutoparkContext(connection);
        }

        public IRepository<VehicleType> VehicleTypes 
        { 
            get
            {
                if (vehicleTypeRepo == null)
                    vehicleTypeRepo = new VehicleTypeRepository(db);
                return vehicleTypeRepo;
            }
        }

        public IRepository<Vehicle> Vehicles 
        { 
            get
            {
                if (vehicleRepo == null)
                    vehicleRepo = new VehicleRepository(db);
                return vehicleRepo;
            }
        }

        public IRepository<SparePart> SpareParts 
        { 
            get
            {
                if (sparePartRepo == null)
                    sparePartRepo = new SparePartRepository(db);
                return sparePartRepo;
            }
        }

        public IRepository<Order> Orders
        { 
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepository(db);
                return orderRepo;
            }
        }

        public IRepository<OrderItem> OrderItems 
        { 
            get
            {
                if (orderItemsRepo == null)
                    orderItemsRepo = new OrderItemsRepository(db);
                return orderItemsRepo;
            }
        }

        public async void Save()
        {
            await db.SaveChangesAsync();
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
