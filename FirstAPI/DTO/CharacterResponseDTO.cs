namespace FirstAPI.DTO;

public class CharacterResponseDTO
{
        public int Id {get; set;}
        public string Name { get; set; } = null!;

                                           
        public PlanetResponseDTO? Planet { get; set; } 

        public int Rating { get; set; }
        public string ElementName { get; set; } = null!;

        public string PathName {get; set; } = null!;

        public string? ImageUrl { get; set; }
}