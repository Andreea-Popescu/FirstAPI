using FirstAPI.models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Character>().HasData(
            new Character
        {
            Id = 1,
            Name = "Phainon",
            Planet = "Amphoreus",
            Rating = 100
        },
        new Character
        {
            Id = 2,
            Name = "Sunday",
            Planet = "Penacony",
            Rating = 8
        },
        new Character
        {
            Id = 3,
            Name = "Sparxie",
            Planet = "Planarcadia",
            Rating = 1
        },
        new Character
        {
            Id = 4,
            Name = "Fugue",
            Planet = "Xianzhou",
            Rating = 7
        }
        );
    }
    public DbSet<Character> Characters => Set<Character>();
}