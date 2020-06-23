using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project.DAL.EntityModels;

namespace Project.DAL.Context
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VersionInfo> VersionInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=192.168.1.5;Port=5432;User ID=postgres;Password=denis;Database=monotest;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("citext")
                .HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.DateCreated).HasDefaultValueSql("now()");

                entity.Property(e => e.DateJoined).HasColumnType("date");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateUpdated).HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("citext");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("true");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnType("citext");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Salary).HasColumnType("numeric(11,2)");
            });

            modelBuilder.Entity<VersionInfo>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Version)
                    .HasName("UC_Version")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(1024);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
