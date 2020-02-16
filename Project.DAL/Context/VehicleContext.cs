using Microsoft.EntityFrameworkCore;
using Project.DAL.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL.Context
{
    public class VehicleContext : DbContext, IDbContext
    {
        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options)
        {

        }



        public DbSet<VehicleMakeEntity> VehicleMakes { get; set; }
        public DbSet<VehicleModelEntity> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // table names
            modelBuilder.Entity<VehicleMakeEntity>().ToTable("VehicleMake");
            modelBuilder.Entity<VehicleModelEntity>().ToTable("VehicleModel");

            // properties
            modelBuilder.Entity<VehicleMakeEntity>().Property(x => x.Name).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<VehicleMakeEntity>().Property(x => x.Abrv).HasMaxLength(15).IsRequired();

            modelBuilder.Entity<VehicleModelEntity>().Property(x => x.Name).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<VehicleModelEntity>().Property(x => x.Abrv).HasMaxLength(15).IsRequired();


            //relationships
            modelBuilder.Entity<VehicleModelEntity>().HasOne(x => x.VehicleMakeEntity).WithMany(x => x.VehicleModels).OnDelete(DeleteBehavior.Cascade).IsRequired();

            base.OnModelCreating(modelBuilder);
        }


    }
}
