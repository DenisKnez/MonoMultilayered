﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Project.DAL;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.DAL.EntityModels;

namespace Project.DAL.ContextConfigurations
{
    public class CompanyTypeConfiguration : IEntityTypeConfiguration<CompanyType>
    {
        public void Configure(EntityTypeBuilder<CompanyType> entity)
        {
            entity.HasIndex(e => e.Abrv)
                .HasName("index_companytype_abrv")
                .IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v1()");

            entity.Property(e => e.Abrv)
                .IsRequired()
                .HasColumnType("citext");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("true");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("citext");
        }
    }
}
