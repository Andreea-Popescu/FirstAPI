using FirstAPI.Data;
using FirstAPI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FirstAPI.Controllers;
using FirstAPI.DTO;

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


[HttpGet("{id}/details")]
public async Task <ActionResult<CharacterResponseDTO>> ShowDetailsCharacter(int id)
    {
        var character = await _context.Characters // Vreau sa interoghez tabela Characters
        .Include(c => c.Planet) // Instructiunea de JOIN -> uita-te la cheia externa PlanetId si adu datele planetei asociate (LEFT JOIN)
        .FirstOrDefaultAsync(c => c.Id == id); // c => c.Id == id | WHERE c.Id = @id LIMIT 1 - conditie cautare
                                                // FirstOrDefaultAsync -> returneaza primu personaj gasit cu acel id, 
                                                // daca nu gaseste personaj cu id-ul ala, returneaza null (val implicita obiecte)
                                                //TRADUCERE SQL:
                                                // SELECT c.Id, c.Name, c.Rating, c.PlanetId, 
                                                // p.Id, p.Name, p.Description
                                                // FROM Characters AS c
                                                // LEFT JOIN Planets AS p ON c.PlanetId = p.Id
                                                // WHERE c.Id = @id
                                                // LIMIT 1;

        if(character == null)
            return NotFound();
        
        // Aici usually ar fi returnat obiectul simplu de caracter cu lista atributelor la planeta, dar am zis ca vrem sa excludem din
        // rezultatul final id-ul caracterului si caracterele planetei de unde vine, ca sa facem asta, transformam obiectu in DTO
        var response = new CharacterResponseDTO
        {
            Name = character.Name,
            Rating = character.Rating,
            Planet = character.Planet == null ? null : new PlanetResponseDTO // operatoru ternar, daca exista planeta o transforma in obiect dto nice
            {
                Name = character.Planet.Name,
                Description = character.Planet.Description
            }
        };

        return Ok(response);
    }
}