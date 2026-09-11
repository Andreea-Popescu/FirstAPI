using FirstAPI.DTO;
using FirstAPI.models;
namespace FirstAPI.Services;

public interface ICharacterService
{
    Task<List<CharacterResponseDTO>> GetAllCharactersAsync();
    Task<CharacterResponseDTO?> GetCharacterByIdAsync(int id); // ? - pt in cazu in care id-ul nu exista, sa returneze null
    Task <bool> DeleteCharacter(int id);
    Task<CharacterResponseDTO> CreateCharacterAsync (CreateCharacterDTO request);
    Task<bool> UpdateCharacterAsync (int id, CreateCharacterDTO updatedCharacter);
    Task<List<Planet>> GetPlanetsAsync();
    Task<List<Element>> GetElementsAsync ();
}

// Interfata - meniul pe care il citeste controllerul, mentioneaza ce operatiuni exista, fara codul de implementare
// AKA MENIUL DE RESTAURANT