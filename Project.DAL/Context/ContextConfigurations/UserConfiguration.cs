﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DAL.EntityModels;

namespace Project.DAL.ContextConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
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

            entity.HasOne(d => d.Company)
                .WithMany(p => p.User)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_user_company");
        }
    }
}
