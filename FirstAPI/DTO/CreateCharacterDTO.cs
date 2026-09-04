namespace FirstAPI.DTO;

public class CreateCharacterDTO
{
    public string Name { get; set; } = null!;
    public int Rating { get; set; }
    public int planetId { get; set; }
}