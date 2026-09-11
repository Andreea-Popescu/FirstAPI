using Microsoft.EntityFrameworkCore;
using FirstAPI.Data;
using FirstAPI.DTO;
using FirstAPI.models;

namespace FirstAPI.Services;

public class LightconeService : ILightconeService
{
    private readonly AppDbContext _context;

    // Dependency Injection
    public LightconeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LightconeResponseDTO>> GetAllLightconesAsync ()
    {
        return await _context.Lightcones
        .Include(c => c.Path)
        .Select(c => new LightconeResponseDTO
        {
            Id = c.Id,
            Name = c.Name,
            Rarity = c.Rarity,
            BaseAtk = c.BaseAtk,
            PathName = c.Path != null ? c.Path.Name : "Unknown",
            pathId = c.PathId
        }).ToListAsync();
    }

    public async Task<LightconeResponseDTO?> GetLightconeByIdAsync (int id)
    {
        return await _context.Lightcones
        .Include(c => c.Path)
        .Where(c => c.Id == id)
        .Select(c => new LightconeResponseDTO
        {
            Id = c.Id,
            Name = c.Name,
            Rarity = c.Rarity,
            BaseAtk = c.BaseAtk,
            PathName = c.Path != null ? c.Path.Name : "Unknown",
            pathId = c.PathId
        }).FirstOrDefaultAsync();
    }

    public async Task<List<models.Path>> GetPathsAsync()
    {
        return await _context.Paths.ToListAsync();
    }

    public async Task<bool> DeleteLightcone(int id)
    {
        var lightcone = await _context.Lightcones.FindAsync(id);
        if (lightcone == null)
        {
            return false;
        }

        _context.Lightcones.Remove(lightcone);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<LightconeResponseDTO> CreateLightcone (CreateLightconeDTO request)
    {
        var lightcone = new Lightcone
        {
            Name = request.Name,
            Rarity = request.Rarity,
            BaseAtk = request.BaseAtk,
            PathId = request.PathId
        };

        _context.Lightcones.Add(lightcone);
        await _context.SaveChangesAsync();

        await _context.Entry(lightcone).Reference(c => c.Path).LoadAsync();

        return new LightconeResponseDTO
        {
            Id = lightcone.Id,
            Name = lightcone.Name,
            Rarity = lightcone.Rarity,
            BaseAtk = lightcone.BaseAtk,
            PathName = (await _context.Paths.FindAsync(lightcone.PathId))?.Name ?? "Unknown"
        };
    }

    public async Task<bool> UpdateLightcone (int id, CreateLightconeDTO updatedLightcone)
    {
        var lightcone = await _context.Lightcones.FindAsync(id);
        if (lightcone == null)
        {
            return false;
        }

        lightcone.Name = updatedLightcone.Name;
        lightcone.Rarity = updatedLightcone.Rarity;
        lightcone.BaseAtk = updatedLightcone.BaseAtk;
        lightcone.PathId = updatedLightcone.PathId;

        await _context.SaveChangesAsync();
        return true;
    }
}

