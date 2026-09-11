using Microsoft.EntityFrameworkCore;
using FirstAPI.Data;
using FirstAPI.DTO;
using FirstAPI.models;

namespace FirstAPI.Services;

public class CharacterService : ICharacterService
{
    private readonly AppDbContext _context;

    // Dependency Injection
    public CharacterService(AppDbContext context)
    {
        _context = context;
    }
    // done

    public async Task<List<CharacterResponseDTO>> GetAllCharactersAsync()
    {
        return await _context.Characters // ?
        .Include(c => c.Planet)
        .Include(c => c.Element)
        .Include(c => c.Path)
        .Select(c => new CharacterResponseDTO {
            Id = c.Id,
            Name = c.Name,
            Rating = c.Rating,
            ElementName = c.Element != null ? c.Element.Name : "Necunoscut",   
            PathName = c.Path != null ? c.Path.Name : "Unknown",   
            ImageUrl = c.ImageUrl,  
            Planet = c.Planet == null ? null : new PlanetResponseDTO
            {
                Name = c.Planet.Name,
                Description = c.Planet.Description
            }
        }).ToListAsync(); // diferenta tolist vs tolistasync?
    }

    public async Task<CharacterResponseDTO?> GetCharacterByIdAsync (int id)
    {
        return await _context.Characters
        .Include(c => c.Planet)
        .Include(c => c.Path)
        .Include(c => c.Element)
        .Where(c => c.Id == id)
        .Select(c => new CharacterResponseDTO 
        {
            Id = c.Id,
            Name = c.Name,
            Rating = c.Rating,
            ImageUrl = c.ImageUrl,  
            PlanetId = c.PlanetId,
            PathId = c.PathId,
            ElementId = c.ElementId,
            ElementName = c.Element != null ? c.Element.Name : "Necunoscut",   
            PathName = c.Path != null ? c.Path.Name : "Unknown",   
            Planet = c.Planet == null ? null : new PlanetResponseDTO
            {
                Name = c.Planet.Name,
                Description = c.Planet.Description
            }
        })
        .FirstOrDefaultAsync();
    }

    public async Task <bool> DeleteCharacter (int id) // bool pt ca service-ul nu vrea statusuri http, doar daca a mers sau nu
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
            return false;

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<CharacterResponseDTO> CreateCharacterAsync(CreateCharacterDTO request)
    {
        var character = new Character
        {
            Name = request.Name,
            Rating = request.Rating,
            ImageUrl = request.ImageUrl,
            ElementId = request.ElementId,
            PathId = request.PathId,
            PlanetId = request.planetId
        };

        _context.Characters.Add(character);
        await _context.SaveChangesAsync();

        await _context.Entry(character).Reference(c => c.Planet).LoadAsync(); // dc? pt ca atunci cand se creeaza are doar id-urile
                                                                    // de la celelalte tabele, cu loadasync cerem EF sa aduca denumirile asociate
                                                                    // ca ele sa existe in characterresponsedto
        await _context.Entry(character).Reference(c => c.Path).LoadAsync();

        return new CharacterResponseDTO
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
        };

    }

    public async Task<bool> UpdateCharacterAsync (int id, CreateCharacterDTO updatedCharacter)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
        {
            return false;
        }

        character.ImageUrl = updatedCharacter.ImageUrl;
        character.Name = updatedCharacter.Name;
        character.Rating = updatedCharacter.Rating;
        character.PlanetId = updatedCharacter.planetId;
        character.ElementId = updatedCharacter.ElementId;
        character.PathId = updatedCharacter.PathId;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<Planet>> GetPlanetsAsync ()
    {
        return await _context.Planets.ToListAsync();
    }

    public async Task<List<Element>> GetElementsAsync ()
    {
        return await _context.Elements.ToListAsync();
    }
}
// AKA BUCATARUL!!!! BUCATARIA = BD