using ECommerce.DataAccess.Identity;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess.Data
{
    public class ECommerceDbContext:IdentityDbContext<ApplicationUser, ApplicationRole, string >
    {
        public ECommerceDbContext (DbContextOptions<ECommerceDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id= 1, Name="Books", Description="GoodBooks",CreateDate= new DateTime(2026, 01,01)},
                new Category { Id= 2, Name="Cloths", Description="Good Books",CreateDate= new DateTime(2026, 01,01)},
                new Category { Id= 3, Name="Electronics", Description="Good Books",CreateDate= new DateTime(2026, 01,01)}
                );
            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p=>p.Products)
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            
            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.Customer)
            //    .WithMany(c => c.Orders)
            //    .HasForeignKey(o => o.CustomerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o=> o.Product)
                .WithMany(o=> o.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
