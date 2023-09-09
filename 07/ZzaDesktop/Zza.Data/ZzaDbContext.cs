using Microsoft.EntityFrameworkCore;

namespace Zza.Data;

public class ZzaDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public DbSet<OrderItemOption> OrderItemOptions { get; set; } = null!;
    public DbSet<ProductOption> ProductOptions { get; set; } = null!;
    public DbSet<ProductSize> ProductSizes { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatuses { get; set; } = null!;

    public ZzaDbContext(DbContextOptions<ZzaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Client must set the ID.
        modelBuilder.Entity<Customer>().Property(c => c.Id).ValueGeneratedNever();

        // Globally disable the convention for cascading deletes
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
    }
}
