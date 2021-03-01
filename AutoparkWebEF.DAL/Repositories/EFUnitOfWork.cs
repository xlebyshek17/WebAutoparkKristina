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
        
        // TODO: Why do all props have interface type but these fields are not? Change this pls.
        private IGenericRepository<Vehicle> vehicleRepo;
        private IGenericRepository<VehicleType> vehicleTypeRepo;
        private IGenericRepository<SparePart> sparePartRepo;
        private IGenericRepository<Order> orderRepo;
        private IGenericRepository<OrderItem> orderItemsRepo;

        public EFUnitOfWork(DbContextOptions<AutoparkContext> connection)
        {
            // TODO: Move it in DI. You have to get ready context instance from DI.
            db = new AutoparkContext(connection);
        }

        public IGenericRepository<VehicleType> VehicleTypes 
        { 
            get
            {
                if (vehicleTypeRepo == null)
                    vehicleTypeRepo = new VehicleTypeRepository(db);
                return vehicleTypeRepo;
            }
        }

        public IGenericRepository<Vehicle> Vehicles 
        { 
            get
            {
                if (vehicleRepo == null)
                    vehicleRepo = new VehicleRepository(db);
                return vehicleRepo;
            }
        }

        public IGenericRepository<SparePart> SpareParts 
        { 
            get
            {
                if (sparePartRepo == null)
                    sparePartRepo = new SparePartRepository(db);
                return sparePartRepo;
            }
        }

        public IGenericRepository<Order> Orders
        { 
            get
            {
                if (orderRepo == null)
                    orderRepo = new OrderRepository(db);
                return orderRepo;
            }
        }

        public IGenericRepository<OrderItem> OrderItems 
        { 
            get
            {
                if (orderItemsRepo == null)
                    orderItemsRepo = new OrderItemsRepository(db);
                return orderItemsRepo;
            }
        }

        public void Save()
        {
            db.SaveChanges();
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
