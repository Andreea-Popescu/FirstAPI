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

    // GET: api/character/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CreateCharacterDTO>> GetCharacterByID(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
        {
            return NotFound();
        }

        var dto = new CreateCharacterDTO

        {
            Name = character.Name,
            Rating = character.Rating,
            planetId = character.PlanetId,
            ElementId = character.ElementId,
            PathId = character.PathId,
            ImageUrl = character.ImageUrl
        };

        return Ok(dto);
    }

    // PUT: api/character/id
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCharacter(int id, [FromBody] CreateCharacterDTO updatedCharacter)
    {
        // 1. Găsește caracterul existent în baza de date
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
        {
            return NotFound("Personajul nu a fost găsit.");
        }

        // 2. Verifică dacă noua planetă și noul element există
        var planetExists = await _context.Planets.AnyAsync(p => p.Id == updatedCharacter.planetId);
        if (!planetExists)
        {
            return BadRequest("Planeta specificată nu există.");
        }

        var elementExists = await _context.Elements.AnyAsync(e => e.Id == updatedCharacter.ElementId);
        if (!elementExists)
        {
            return BadRequest("Elementul specificat nu există.");
        }

        var pathExists = await _context.Paths.AnyAsync(h => h.Id == updatedCharacter.PathId);
        if (!pathExists)
        {
            return BadRequest("Path-ul specificat nu există.");
        }

        // 3. Actualizează câmpurile entității existente cu datele din DTO
        character.ImageUrl = updatedCharacter.ImageUrl;
        character.Name = updatedCharacter.Name;
        character.Rating = updatedCharacter.Rating;
        character.PlanetId = updatedCharacter.planetId;
        character.ElementId = updatedCharacter.ElementId;
        character.PathId = updatedCharacter.PathId;

        // 4. Salvează modificările în SQLite
        await _context.SaveChangesAsync();

        // 5. Răspunde cu succes (204 No Content este standardul pentru PUT)
        return NoContent();
    }

    // DELETE: api/character/1 stergere caracter
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
            return NotFound();

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        return NoContent();
    }

// GET api/character/id/details
[HttpGet("{id:int}/details")]
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
            ImageUrl = character.ImageUrl,
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


// GET: api/character
[HttpGet]
public async Task<ActionResult<List<CharacterResponseDTO>>> GetCharacters()
{
    var characters = await _context.Characters
        .Include(c => c.Planet) // JOIN cu tabela Planets pentru toată lista
        .Include (c => c.Element) 
        .Include (c => c.Path) // join cu tabela Paths (ob Pathh)
        .ToListAsync();

    // Mapăm fiecare personaj din listă către CharacterResponseDTO
    var response = characters.Select(character => new CharacterResponseDTO
    {
        Id = character.Id,
        Name = character.Name,
        Rating = character.Rating,
        ElementName = character.Element != null ? character.Element.Name : "Necunoscut",   
        PathName = character.Path != null ? character.Path.Name : "Unknown",   
        ImageUrl = character.ImageUrl,  
        Planet = character.Planet == null ? null : new PlanetResponseDTO
        {
            Name = character.Planet.Name,
            Description = character.Planet.Description
        }
    }).ToList();

    return Ok(response);
}


// POST: api/character
[HttpPost]
public async Task<ActionResult<CharacterResponseDTO>> CreateCharacter (CreateCharacterDTO request)
    {
        var planet = await _context.Planets.FindAsync(request.planetId); // exista planeta ceruta?
        if (planet == null)
        {
            return BadRequest("Nu exista planeta asta wtf");
        }
        var element = await _context.Elements.FindAsync(request.ElementId); // exista elementul cerut?
        if (element == null)
        {
            return BadRequest("Nu exista elementul");
        }

        var path = await _context.Paths.FindAsync(request.PathId); 
        if (path == null)
        {
            return BadRequest("Nu exista Path-ul lol");
        }

        var newCharacter = new Character // cream un nou ob caracter cu atributele date de user
        {
            ImageUrl = request.ImageUrl,
            Name = request.Name,
            Rating = request.Rating,
            PlanetId = request.planetId,
            ElementId = request.ElementId,
            PathId = request.PathId
        };

        _context.Characters.Add(newCharacter); // incarcam in context pt tabela characters noul caracter
        await _context.SaveChangesAsync(); // trimitem in tabela

        var response = new CharacterResponseDTO // returneaza caracterul adaugat
        {
            Name = newCharacter.Name,
            Rating = newCharacter.Rating,
            Planet = new PlanetResponseDTO // fiind characterresponsedto include planetresponsedto
            {
                Name = planet.Name,
                Description = planet.Description
            },
            ElementName = element.Name,
            PathName = path.Name
        };

        return Ok(response);
    }

// GET pt planete: api/character/planets
[HttpGet("planets")]
public async Task<ActionResult<List<Planet>>> GetPlanets ()
    {
        var planets = await _context.Planets.ToListAsync();
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
        var elements = await _context.Elements.ToListAsync();
        if (elements == null)
        {
            return NotFound();
        }
        return Ok(elements);
    }
}