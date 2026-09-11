using Microsoft.AspNetCore.Mvc;
namespace FirstAPI.Controllers;
using Path = FirstAPI.models.Path;
using FirstAPI.DTO;
using FirstAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class LightconeController : ControllerBase
{
    private readonly ILightconeService _lightconeService;

    // 1. Injectăm AppDbContext prin Dependency Injection
    public LightconeController(ILightconeService lightconeService)
    {
        _lightconeService = lightconeService;
    }

    // GET: api/lightcone
    [HttpGet]
    public async Task<ActionResult<CreateLightconeDTO>> GetLightcone ()
    {
        var lightcones = await _lightconeService.GetAllLightconesAsync();
        return Ok(lightcones);
    }

    // GET: api/lightcone/id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CreateLightconeDTO>> GetLightconeByID (int id)
    {
        var lightcone = await _lightconeService.GetLightconeByIdAsync(id);
        if(lightcone == null)
        {
            return NotFound();
        }
        return Ok(lightcone);
    }

    // GET: api/lightcone/paths
    [HttpGet("paths")]
    public async Task<ActionResult<List<Path>>> GetPaths ()
    {
        var paths = await _lightconeService.GetPathsAsync();
        return Ok(paths);
    }

    // DELETE: api/lightcone/id
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLightcone(int id)
    {
        var result = await _lightconeService.DeleteLightcone(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    // POST: api/lightcone
    [HttpPost]
    public async Task<ActionResult<LightconeResponseDTO>> CreateLightcone (CreateLightconeDTO request)
    {
        var lightcone = await _lightconeService.CreateLightcone(request);
        return Ok(lightcone);
    }


    // PUT: api/lightcone/id
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLightcone (int id, [FromBody] CreateLightconeDTO updatedLightcone)
    {
        var updated = await _lightconeService.GetLightconeByIdAsync(id);
        if (updated == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}