using FirstAPI.Data;
using FirstAPI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FirstAPI.Controllers;

using Path = FirstAPI.models.Path;
using System.Reflection.Metadata.Ecma335;
using FirstAPI.DTO;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class LightconeController : ControllerBase
{
    private readonly AppDbContext _context;

    // 1. Injectăm AppDbContext prin Dependency Injection
    public LightconeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/lightcone
    [HttpGet]
    public async Task<ActionResult<CreateLightconeDTO>> GetLightcone ()
    {
        var lightcones = await _context.Lightcones
        .Include(lc => lc.Path)
        .ToListAsync();


        var response = lightcones.Select(lightcone => new LightconeResponseDTO
    {
        Id = lightcone.Id,
        Name = lightcone.Name,
        Rarity = lightcone.Rarity,
        BaseAtk = lightcone.BaseAtk,
        PathName = lightcone.Path != null ? lightcone.Path.Name : "Unknown"
    }).ToList();

    return Ok(response);
    }

    // GET: api/lightcone/id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CreateLightconeDTO>> GetLightconeByID (int id)
    {
        var lightcone = await _context.Lightcones.FindAsync(id);
        if (lightcone == null)
        {
            return NotFound();
        }
        var responseid = new CreateLightconeDTO
        {
          Name = lightcone.Name,
          Rarity = lightcone.Rarity,
          BaseAtk = lightcone.BaseAtk,
          PathId = lightcone.PathId  
        };
        return Ok(responseid);
    }

    // GET: api/lightcone/paths
    [HttpGet("paths")]
    public async Task<ActionResult<List<Path>>> GetPaths ()
    {
        var paths = await _context.Paths.ToListAsync();
        if (paths == null)
        {
            return NotFound();
        }
        return Ok(paths);
    }

    // DELETE: api/lightcone/id
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLightcone(int id)
    {
        var lightcone = await _context.Lightcones.FindAsync(id);
        if(lightcone == null)
        {
            return NotFound();
        }
        _context.Lightcones.Remove(lightcone);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // POST: api/lightcone
    [HttpPost]
    public async Task<ActionResult<LightconeResponseDTO>> CreateLightcone (CreateLightconeDTO request)
    {
        var path = await _context.Paths.FindAsync(request.PathId);
        if (path == null)
        {
            return BadRequest("Nu exista path-ul asta!");
        }
        var newLightcone = new Lightcone
        {
            Name = request.Name,
            Rarity = request.Rarity,
            BaseAtk = request.BaseAtk,
            PathId = request.PathId
        };

        _context.Lightcones.Add(newLightcone);
        await _context.SaveChangesAsync();
        var response = new LightconeResponseDTO
        {
            Name = newLightcone.Name,
            Rarity = newLightcone.Rarity,
            BaseAtk = newLightcone.BaseAtk,
            PathName = path.Name
        };
        
        return Ok(response);
    }

    // PUT: api/lightcone/id
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLightcone (int id, [FromBody] CreateLightconeDTO updatedLightcone)
    {
        var lightcone = await _context.Lightcones.FindAsync(id);
        if (lightcone == null)
        {
            return NotFound("Lightcone-ul nu a fost găsit.");
        }
        var pathExists = await _context.Paths.AnyAsync(p => p.Id == updatedLightcone.PathId);
        if (!pathExists)
        {
            return BadRequest("Path-ul specificat nu există.");
        }

        lightcone.Name = updatedLightcone.Name;
        lightcone.Rarity = updatedLightcone.Rarity;
        lightcone.BaseAtk = updatedLightcone.BaseAtk;
        lightcone.PathId = updatedLightcone.PathId;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}