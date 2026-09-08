namespace FirstAPI.DTO;

public class CreateCharacterDTO
{
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    public int planetId { get; set; }
    public int ElementId {get; set; }

    public int PathId {get; set;}

    public string? ImageUrl { get; set; }
}