namespace FirstUI.DTO;

public class CharacterResponseDTO
{
        public int Id { get; set;}
        public string Name { get; set; } = null!;

                                           
        public PlanetResponseDTO? Planet { get; set; } 

        public int Rating { get; set; }
        public string ElementName {get; set;} = null!;
        public string PathName {get; set;} = null!;

        public string? ImageUrl { get; set; }
        public int PlanetId { get; set; }
        public int PathId { get; set; }
        public int ElementId { get; set; }
} // same structure ca la firstapi pt ca blazor trb sa stie cum sa despacheteze jsonu de la api (de aia avem iar dtos aici)