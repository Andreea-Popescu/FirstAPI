using Microsoft.EntityFrameworkCore;
using FirstAPI.Data;
using FirstAPI.DTO;

namespace FirstAPI.Services;

public class LightconeService : ILightconeService
{
    private readonly AppDbContext _context;

    // Dependency Injection
    public LightconeService(AppDbContext context)
    {
        _context = context;
    }
}