using Microsoft.EntityFrameworkCore;

namespace Product.DAL.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Domain.DbSet.Product> Products { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}