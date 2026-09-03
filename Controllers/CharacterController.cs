using FirstAPI.Data;
using FirstAPI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly AppDbContext _context;

    // 1. Injectăm AppDbContext prin Dependency Injection
    public CharacterController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/character
    [HttpGet]
    public async Task<ActionResult<List<Character>>> GetCharacters()
    {
        var characters = await _context.Characters.ToListAsync();
        return Ok(characters);
    }

    // GET: api/character/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Character>> GetCharacterByID(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
            return NotFound();

        return Ok(character);
    }

    // POST: api/character
    [HttpPost]
    public async Task<ActionResult<Character>> AddCharacter([FromBody] Character newCharacter)
    {
        if (newCharacter == null)
            return BadRequest();

        // Salvează în tabelul SQLite
        _context.Characters.Add(newCharacter);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCharacterByID), new { id = newCharacter.Id }, newCharacter);
    }

    // PUT: api/character/1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCharacter(int id, [FromBody] Character updatedCharacter)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
            return NotFound();

        character.Name = updatedCharacter.Name;
        character.Planet = updatedCharacter.Planet;
        character.Rating = updatedCharacter.Rating;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/character/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
            return NotFound();

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}