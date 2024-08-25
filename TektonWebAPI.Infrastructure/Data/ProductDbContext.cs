using Microsoft.EntityFrameworkCore;

namespace TektonWebAPI.Infrastructure.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}