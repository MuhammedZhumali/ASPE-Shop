using E_Shop.Models.Entities;
using E_Shop.Models.Entities.OrderFolder;
using Microsoft.EntityFrameworkCore;
using Questionnaire.Models.Entities.OrderFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models
{
  public class UsersContext : DbContext
  {
    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }

    //Orders
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderStatus> OrderStatuses { get; set; }
    public DbSet<OrderStock> OrderStocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //Fluent API по большому счету представляет набор методов, которые определяются сопоставление между классами и их свойствами и таблицами и их столбцами.
      base.OnModelCreating(modelBuilder);
      modelBuilder.Entity<OrderStock>()
          .HasKey(x => new { x.StockId, x.OrderId });
    }
  }
}
