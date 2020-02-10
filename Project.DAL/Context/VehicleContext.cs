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



        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // table names
            modelBuilder.Entity<VehicleMake>().ToTable("VehicleMake");
            modelBuilder.Entity<VehicleModel>().ToTable("VehicleModel");

            // properties
            modelBuilder.Entity<VehicleMake>().Property(x => x.Name).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<VehicleMake>().Property(x => x.Abrv).HasMaxLength(15).IsRequired();

            modelBuilder.Entity<VehicleModel>().Property(x => x.Name).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<VehicleModel>().Property(x => x.Abrv).HasMaxLength(15).IsRequired();


            //relationships
            modelBuilder.Entity<VehicleModel>().HasOne(x => x.VehicleMake).WithMany(x => x.VehicleModels).OnDelete(DeleteBehavior.Cascade).IsRequired();

            base.OnModelCreating(modelBuilder);
        }


    }
}
