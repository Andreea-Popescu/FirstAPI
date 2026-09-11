using FirstAPI.Data;
using FirstAPI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FirstAPI.Controllers;
using FirstAPI.DTO;
using FirstAPI.Services; // includem si aici ca sa stie controlleru

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    //  Acm facem dependency injection cu iservice (meniul) !!
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    // GET: api/character/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CharacterResponseDTO>> GetCharacterByID(int id)
    {
        var character = await _characterService.GetCharacterByIdAsync(id);
        if (character == null)
        {
            return NotFound();
        }

        return Ok(character);
    }

    // PUT: api/character/id
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CreateCharacterDTO updatedCharacter)
    {
        var updated = await _characterService.UpdateCharacterAsync(id, updatedCharacter);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/character/1 stergere caracter
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var success = await _characterService.DeleteCharacter(id);
        if(!success)
        {
            return NotFound();
        }

        return NoContent();
    }


// GET: api/character
[HttpGet]
public async Task<ActionResult<List<CharacterResponseDTO>>> GetCharacters()
{
    var characters = await _characterService.GetAllCharactersAsync();
    return Ok(characters);
}


// POST: api/character
[HttpPost]
public async Task<ActionResult<CharacterResponseDTO>> CreateCharacter ([FromBody] CreateCharacterDTO request)
    {
        var newcharacter = _characterService.CreateCharacterAsync(request);

        return Ok(newcharacter);
    }

// GET pt planete: api/character/planets
[HttpGet("planets")]
public async Task<ActionResult<List<Planet>>> GetPlanets ()
    {
        var planets = await _characterService.GetPlanetsAsync();
        if (planets == null)
        {
            return NotFound();
        }
        return Ok(planets);
    }

// GET pt elemente: api/character/elements
[HttpGet("elements")]
public async Task<ActionResult<List<Element>>> GetElements ()
    {
        var elements = await _characterService.GetElementsAsync();
        if (elements == null)
        {
            return NotFound();
        }
        return Ok(elements);
    }
}