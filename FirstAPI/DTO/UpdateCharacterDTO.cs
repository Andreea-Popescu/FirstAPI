namespace FirstAPI.DTO;

public class UpdateCharacterDTO
{
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    public int planetId { get; set; }
    public int ElementId {get; set; }
}