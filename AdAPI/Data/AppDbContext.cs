using AdAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Ad> Ads { get; set; }
}