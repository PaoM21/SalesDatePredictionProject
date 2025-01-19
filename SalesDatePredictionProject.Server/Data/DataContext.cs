using Microsoft.EntityFrameworkCore;
using SalesDatePredictionProject.Server.Dto;
using SalesDatePredictionProject.Server.Models;
using System.Diagnostics.Metrics;

namespace SalesDatePredictionProject.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<OrderDetails> OrdersDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>()
                .ToTable("Customers", "Sales");
            modelBuilder.Entity<Customers>()
                .HasKey(c => c.CustId);
            modelBuilder.Entity<Employees>()
                .ToTable("Employees", "HR");
            modelBuilder.Entity<Employees>()
                .HasKey(e => e.EmpId);
            modelBuilder.Entity<OrderDetails>()
                .ToTable("OrderDetails", "Sales");
            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => new { od.OrderId, od.ProductId });
            modelBuilder.Entity<Orders>()
                .ToTable("Orders", "Sales");
            modelBuilder.Entity<Orders>()
                .HasKey(or => or.OrderId);
            modelBuilder.Entity<Products>()
                .ToTable("Products", "Production");
            modelBuilder.Entity<Products>()
                .HasKey(or => or.ProductId);
            modelBuilder.Entity<Shippers>()
                .ToTable("Shippers", "Sales");
            modelBuilder.Entity<Shippers>()
                .HasKey(or => or.ShipperId);
        }
    }
}
