using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data { 

public partial class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    




    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customerdemographic> Customerdemographics { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderdetail> Orderdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.HasIndex(e => e.CategoryName, "Categories_CategoryName");

            entity.Property(e => e.CategoryId)
                .HasColumnType("int(11)")
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(15);
            entity.Property(e => e.Description).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Picture).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.HasIndex(e => e.City, "Customers_City");

            entity.HasIndex(e => e.CompanyName, "Customers_CompanyName");

            entity.HasIndex(e => e.PostalCode, "Customers_PostalCode");

            entity.HasIndex(e => e.Region, "Customers_Region");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ContactTitle)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");

            entity.HasMany(d => d.CustomerTypes).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "Customercustomerdemo",
                    r => r.HasOne<Customerdemographic>().WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_CustomerCustomerDemo"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_CustomerCustomerDemo_Customers"),
                    j =>
                    {
                        j.HasKey("CustomerId", "CustomerTypeId").HasName("PRIMARY");
                        j.ToTable("customercustomerdemo");
                        j.HasIndex(new[] { "CustomerTypeId" }, "FK_CustomerCustomerDemo");
                        j.HasIndex(new[] { "CustomerId" }, "FK_CustomerCustomerDemo_Customers");
                        j.IndexerProperty<string>("CustomerId")
                            .HasMaxLength(5)
                            .IsFixedLength()
                            .HasColumnName("CustomerID");
                        j.IndexerProperty<string>("CustomerTypeId")
                            .HasMaxLength(10)
                            .IsFixedLength()
                            .HasColumnName("CustomerTypeID");
                    });
        });

        modelBuilder.Entity<Customerdemographic>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId).HasName("PRIMARY");

            entity.ToTable("customerdemographics");

            entity.Property(e => e.CustomerTypeId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("CustomerTypeID");
            entity.Property(e => e.CustomerDesc).HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.LastName, "Employees_LastName");

            entity.HasIndex(e => e.PostalCode, "Employees_PostalCode");

            entity.HasIndex(e => e.ReportsTo, "FK_Employees_Employees");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.BirthDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Extension)
                .HasMaxLength(4)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.FirstName).HasMaxLength(10);
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.HomePhone)
                .HasMaxLength(24)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.Notes).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Photo).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ReportsTo)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.TitleOfCourtesy)
                .HasMaxLength(25)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Employees_Employees");

            entity.HasMany(d => d.Territories).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "Employeeterritory",
                    r => r.HasOne<Territory>().WithMany()
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_EmployeeTerritories_Territories"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_EmployeeTerritories_Employees"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "TerritoryId").HasName("PRIMARY");
                        j.ToTable("employeeterritories");
                        j.HasIndex(new[] { "TerritoryId" }, "FK_EmployeeTerritories_Territories");
                        j.IndexerProperty<int>("EmployeeId")
                            .HasColumnType("int(11)")
                            .HasColumnName("EmployeeID");
                        j.IndexerProperty<string>("TerritoryId")
                            .HasMaxLength(20)
                            .HasColumnName("TerritoryID");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.CustomerId, "FK_Orders_Customers");

            entity.HasIndex(e => e.CustomerId, "Orders_CustomerID");

            entity.HasIndex(e => e.CustomerId, "Orders_CustomersOrders");

            entity.HasIndex(e => e.EmployeeId, "Orders_EmployeeID");

            entity.HasIndex(e => e.EmployeeId, "Orders_EmployeesOrders");

            entity.HasIndex(e => e.OrderDate, "Orders_OrderDate");

            entity.HasIndex(e => e.ShipPostalCode, "Orders_ShipPostalCode");

            entity.HasIndex(e => e.ShippedDate, "Orders_ShippedDate");

            entity.HasIndex(e => e.ShipVia, "Orders_ShippersOrders");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .HasDefaultValueSql("'NULL'")
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.EmployeeId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Freight).HasDefaultValueSql("'0'");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.RequiredDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(60)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ShipCity)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ShipCountry)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ShipName)
                .HasMaxLength(40)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ShipPostalCode)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ShipRegion)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ShipVia)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.ShippedDate)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Orders_Customers");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Orders_Employees");

            entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipVia)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Orders_Shippers");
        });

        modelBuilder.Entity<Orderdetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PRIMARY");

            entity.ToTable("orderdetails");

            entity.HasIndex(e => e.OrderId, "OrderDetails_OrderID");

            entity.HasIndex(e => e.OrderId, "OrderDetails_OrdersOrder_Details");

            entity.HasIndex(e => e.ProductId, "OrderDetails_ProductID");

            entity.HasIndex(e => e.ProductId, "OrderDetails_ProductsOrder_Details");

            entity.Property(e => e.OrderId)
                .HasColumnType("int(11)")
                .HasColumnName("OrderID");
            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductID");
            entity.Property(e => e.Quantity).HasColumnType("smallint(6)");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_Details_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.Orderdetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Order_Details_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.CategoryId, "Products_CategoriesProducts");

            entity.HasIndex(e => e.CategoryId, "Products_CategoryID");

            entity.HasIndex(e => e.ProductName, "Products_ProductName");

            entity.HasIndex(e => e.SupplierId, "Products_SupplierID");

            entity.HasIndex(e => e.SupplierId, "Products_SuppliersProducts");

            entity.Property(e => e.ProductId)
                .HasColumnType("int(11)")
                .HasColumnName("ProductID");
            entity.Property(e => e.CategoryId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("CategoryID");
            entity.Property(e => e.ProductName).HasMaxLength(40);
            entity.Property(e => e.QuantityPerUnit)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ReorderLevel)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(6)");
            entity.Property(e => e.SupplierId)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("SupplierID");
            entity.Property(e => e.UnitPrice).HasDefaultValueSql("'0'");
            entity.Property(e => e.UnitsInStock)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(6)");
            entity.Property(e => e.UnitsOnOrder)
                .HasDefaultValueSql("'0'")
                .HasColumnType("smallint(6)");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Products_Categories");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Products_Suppliers");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PRIMARY");

            entity.ToTable("region");

            entity.Property(e => e.RegionId)
                .HasColumnType("int(11)")
                .HasColumnName("RegionID");
            entity.Property(e => e.RegionDescription)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipperId).HasName("PRIMARY");

            entity.ToTable("shippers");

            entity.Property(e => e.ShipperId)
                .HasColumnType("int(11)")
                .HasColumnName("ShipperID");
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.HasIndex(e => e.CompanyName, "Suppliers_CompanyName");

            entity.HasIndex(e => e.PostalCode, "Suppliers_PostalCode");

            entity.Property(e => e.SupplierId)
                .HasColumnType("int(11)")
                .HasColumnName("SupplierID");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.ContactTitle)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.HomePage).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasDefaultValueSql("'NULL'");
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasKey(e => e.TerritoryId).HasName("PRIMARY");

            entity.ToTable("territories");

            entity.HasIndex(e => e.RegionId, "FK_Territories_Region");

            entity.Property(e => e.TerritoryId)
                .HasMaxLength(20)
                .HasColumnName("TerritoryID");
            entity.Property(e => e.RegionId)
                .HasColumnType("int(11)")
                .HasColumnName("RegionID");
            entity.Property(e => e.TerritoryDescription)
                .HasMaxLength(50)
                .IsFixedLength();

            entity.HasOne(d => d.Region).WithMany(p => p.Territories)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Territories_Region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

}