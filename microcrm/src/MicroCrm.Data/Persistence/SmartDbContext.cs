﻿using Microsoft.EntityFrameworkCore;
using MicroCrm.Domain.Models;

namespace MicroCrm.Infrastructure.Persistence
{
  public partial class MicroCrmDbContext : DbContext
  {

    #region Business Domain Entity
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }
    public virtual DbSet<AqDetail> AqDetails { get; set; }
    public virtual DbSet<ContactActivity> ContactActivities { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Quotation> Quotations { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      #region set Global Query Filters with tenantid

      modelBuilder.Entity<Product>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);
      modelBuilder.Entity<Customer>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);

      modelBuilder.Entity<Contact>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);
      modelBuilder.Entity<Project>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);
      modelBuilder.Entity<Quotation>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);
      modelBuilder.Entity<ContactActivity>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);
      modelBuilder.Entity<AqDetail>().HasQueryFilter(b => EF.Property<int>(b, "TenantId") == _tenantId);

      #endregion

      #region Business Domain Entity
      modelBuilder.Entity<Company>(entity =>
      {
        entity.HasIndex(e => e.Name)
                      .IsUnique()
                      .HasFilter(null);
        entity.HasIndex(e => e.Code)
                      .IsUnique()
                      .HasFilter("[Code] IS NOT NULL");
      });
      modelBuilder.Entity<Product>(entity =>
      {
        entity.HasIndex(e => new { e.Name })
                      .IsUnique()
                      .HasFilter(null);

      });
      modelBuilder.Entity<Customer>(entity =>
      {
        entity.HasIndex(e => new { e.Name })
                      .IsUnique()
                      .HasFilter(null);

      });
      modelBuilder.Entity<Order>(entity =>
      {
        entity.HasIndex(e => new { e.OrderNo })
                      .IsUnique()
                      .HasFilter(null);

      });
      #endregion

      #region infrastructure
      modelBuilder.Entity<DataTableImportMapping>(entity =>
      {
        entity.HasIndex(e => new { e.EntitySetName, e.FieldName })
                      .IsUnique()
                      .HasFilter(null);

      });

      modelBuilder.Entity<CodeItem>(entity =>
      {
        entity.HasIndex(e => new { e.CodeType, e.Code })
                      .IsUnique()
                      .HasFilter(null);

      });

      modelBuilder.Entity<RoleMenu>(entity =>
      {
        entity.HasIndex(e => new { e.RoleName, e.MenuId })
                      .IsUnique()
                      .HasFilter(null);

      });
      #endregion
    }
  }
}
