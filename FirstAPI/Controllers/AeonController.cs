namespace FirstAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstAPI.Data;
using FirstAPI.models;
using FirstAPI.DTO;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using FirstUI.DTO;

[ApiController]
[Route("api/[controller]")]

public class AeonController : ControllerBase
{
    private readonly AppDbContext _context;

    // Dependency Injection AICI!
    public AeonController(AppDbContext context)
    {
        _context = context;
    }
    // GATA

    // GET /api/aeon
    [HttpGet]
    public async Task<ActionResult<List<AeonResponseDTO>>> GetAeons ()
    {
        var aeons = await _context.Aeons
        .Include(a => a.Path)
        .ToListAsync();

        var response = aeons.Select(aeon => new AeonResponseDTO
        {
            Id = aeon.Id,
            Name = aeon.Name,
            Description = aeon.Description,
            PathName = aeon.Path != null ? aeon.Path.Name : "Unknown",
            ImageUrl = aeon.ImageUrl
        }).ToList();

        return Ok(response);

    }

    // GET /api/aeon/id
    [HttpGet("{id:int}")]
    public async Task <ActionResult<CreateAeonDTO>> GetAeonByID(int id)
    {
        var aeon = await _context.Aeons.FindAsync(id);
        if (aeon == null)
        {
            return NotFound("Nu am gasit acest Aeon");
        }

        var result = new CreateAeonDTO
        {
            Name = aeon.Name,
            Description = aeon.Description,
            PathId = aeon.PathId,
            ImageUrl = aeon.ImageUrl
        };

        return Ok(result);
    }

    // POST /api/aeon
    [HttpPost]
    public async Task <ActionResult<CreateAeonDTO>> CreateAeon (CreateAeonDTO request)
    {
        var path = await _context.Paths.FindAsync(request.PathId);
        if(path == null)
        {
            return BadRequest("Nu exista acest path");
        }

        var newAeon = new Aeon
        {
            Name = request.Name,
            Description = request.Description,
            PathId = request.PathId,
            ImageUrl = request.ImageUrl
        };

        _context.Aeons.Add(newAeon);
        await _context.SaveChangesAsync();

        var result = new AeonResponseDTO
        {
            Name = newAeon.Name,
            Description = newAeon.Description,
            PathName = path.Name,
            ImageUrl = newAeon.ImageUrl
        };

        return Ok(result);

    }

    // PUT /api/aeon
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAeon (int id, [FromBody] CreateAeonDTO updatedAeon)
    {
        var aeon = await _context.Aeons.FindAsync(id);
        if (aeon == null)
        {
            return NotFound("Nu s-a gasit acest Aeon");
        }

        var pathexists = await _context.Paths.AnyAsync(p => p.Id == updatedAeon.PathId);
        if (!pathexists)
        {
            return BadRequest("Path-ul specificat nu exista");
        }

        aeon.Name = updatedAeon.Name;
        aeon.Description = updatedAeon.Description;
        aeon.PathId = updatedAeon.PathId;
        aeon.ImageUrl = updatedAeon.ImageUrl;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE /api/aeon
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAeon (int id)
    {
        var aeon = await _context.Aeons.FindAsync(id);
        if (aeon == null)
        {
            return NotFound();
        }

        _context.Aeons.Remove(aeon);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET /api/aeon/paths
    [HttpGet("paths")]
    public async Task<ActionResult<List<Path>>> GetPath ()
    {
        var path = await _context.Paths.ToListAsync();
        if (path == null)
        {
            return NotFound();
        }
        return Ok(path);
    }
}