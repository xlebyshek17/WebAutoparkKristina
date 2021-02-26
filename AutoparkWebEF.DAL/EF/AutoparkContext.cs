using System;
using AutoparkWebEF.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AutoparkWebEF.DAL.EF
{
    public partial class AutoparkContext : DbContext
    {
        public AutoparkContext()
        {
        }

        public AutoparkContext(DbContextOptions<AutoparkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<SparePart> SpareParts { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.VehicleId, "IX_Orders_VehicleId");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK__Orders__VehicleI__3B75D760");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasIndex(e => e.DetailId, "IX_OrderItems_DetailId");

                entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.DetailId)
                    .HasConstraintName("FK__OrderItem__Detai__412EB0B6");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderItem__Order__403A8C7D");
            });

            modelBuilder.Entity<SparePart>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasIndex(e => e.TypeId, "IX_Vehicles_TypeId");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Engine)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mileage).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RegistrationNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TankVolume).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Vehicles__TypeId__38996AB5");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.Property(e => e.TaxCoefficient).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
