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

        modelBuilder.Entity<Element>().HasData(
            new Element { Id = 1, Name = "Physical" },
            new Element { Id = 2, Name = "Fire" },
            new Element { Id = 3, Name = "Ice" },
            new Element { Id = 4, Name = "Lightning" },
            new Element { Id = 5, Name = "Wind" },
            new Element { Id = 6, Name = "Quantum" },
            new Element { Id = 7, Name = "Imaginary" }
        );

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
            },
            new Planet
            {
                Id = 5,
                Name = "Belobog",
                Description = "Frozen Leadership"
            },
            new Planet
            {
                Id = 6,
                Name = "Herta Space Station",
                Description = "Geniuses Greetings"
            },
            new Planet
            {
                Id = 7,
                Name = "Astral Express",
                Description = "Path of Trailblazing"
            }
        );

        modelBuilder.Entity<Character>().HasData(
            new Character
        {
            Id = 1,
            Name = "Phainon",
            PlanetId = 1,
            Rating = 100,
            ElementId = 1
        },
        new Character
        {
            Id = 2,
            Name = "Sunday",
            PlanetId = 2,
            Rating = 8,
            ElementId = 7
        },
        new Character
        {
            Id = 3,
            Name = "Sparxie",
            PlanetId = 3,
            Rating = 1,
            ElementId = 2
        },
        new Character
        {
            Id = 4,
            Name = "Fugue",
            PlanetId = 4,
            Rating = 7,
            ElementId = 2
        }
        );
    }
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Planet> Planets => Set<Planet>(); // asa cream tabele
    public DbSet<Element> Elements => Set<Element>();
}