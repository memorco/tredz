using Microsoft.EntityFrameworkCore;

namespace Tredz.MinimalApi.Models;

public class BrandDB : DbContext
{
    public BrandDB(DbContextOptions options) : base(options) { }
    public DbSet<Brand> Brands { get; set; } = null!;
}

