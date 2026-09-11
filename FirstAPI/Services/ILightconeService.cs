using FirstAPI.DTO;
using FirstAPI.models;
namespace FirstAPI.Services;

public interface ILightconeService
{
    Task<List<LightconeResponseDTO>> GetAllLightconesAsync ();
    Task<LightconeResponseDTO?> GetLightconeByIdAsync (int id);
    Task<List<models.Path>> GetPathsAsync();
    Task<bool> DeleteLightcone(int id);
    Task<LightconeResponseDTO> CreateLightcone (CreateLightconeDTO request);
    Task<bool> UpdateLightcone (int id, CreateLightconeDTO updatedLightcone);
}