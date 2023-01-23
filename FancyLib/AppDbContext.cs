using FancyLib.Domain;
using Microsoft.EntityFrameworkCore;

namespace FancyLib;

public class AppDbContext : DbContext
{
    public DbSet<Number> Numbers { get; set; } = null!;
    public DbSet<Text> Texts { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Number>()
                    .HasData(new Number { Id = 9001, Value = 9001 });

        modelBuilder.Entity<Text>()
                    .HasData(new Text { Id = 9001, Value = "OVER NINE THOUSAND" });
    }
}