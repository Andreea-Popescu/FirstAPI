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

        modelBuilder.Entity<Planet>().HasData( // hardcoded date in tabele lol
            new Planet
            {
                Id = 1,
                Name = "Amphoreus",
                Description = "Eternal Land"
            },
            new Planet
            {
                Id = 2,
                Name = "Penacony",
                Description = "Planet of Festivities"
            },
            new Planet
            {
                Id = 3,
                Name = "Planarcadia",
                Description = "Vibrant Dreamscape"
            },
            new Planet
            {
                Id = 4,
                Name = "Xianzhou",
                Description = "Alliance Flagship"
            }
        );

        modelBuilder.Entity<Character>().HasData(
            new Character
        {
            Id = 1,
            Name = "Phainon",
            PlanetId = 1,
            Rating = 100
        },
        new Character
        {
            Id = 2,
            Name = "Sunday",
            PlanetId = 2,
            Rating = 8
        },
        new Character
        {
            Id = 3,
            Name = "Sparxie",
            PlanetId = 3,
            Rating = 1
        },
        new Character
        {
            Id = 4,
            Name = "Fugue",
            PlanetId = 4,
            Rating = 7
        }
        );
    }
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Planet> Planets => Set<Planet>(); // asa cream tabele
}