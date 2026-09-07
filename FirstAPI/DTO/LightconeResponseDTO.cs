namespace FirstAPI.DTO;

public class LightconeResponseDTO
{
        public int Id {get; set;}
        public string Name { get; set; } = null!;
        public int Rarity { get; set; }
        public int BaseAtk { get; set; }
        public string PathName {get; set; } = null!;
        }