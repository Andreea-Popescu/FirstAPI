namespace FirstUI.DTO;

public class CharacterResponseDTO
{
        public string Name { get; set; } = null!;

                                           
        public PlanetResponseDTO? Planet { get; set; } 

        public int Rating { get; set; }
} // same structure ca la firstapi pt ca blazor trb sa stie cum sa despacheteze jsonu de la api (de aia avem iar dtos aici)