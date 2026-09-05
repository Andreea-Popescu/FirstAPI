using FirstAPI.models;

namespace FirstAPI.DTO;

public class CharacterResponseDTO
{
        public string Name { get; set; } = null!;

                                           
        public PlanetResponseDTO? Planet { get; set; } 

        public int Rating { get; set; }
        public string ElementName { get; set; } = null!;
        }