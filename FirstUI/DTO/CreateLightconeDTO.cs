using System.ComponentModel.DataAnnotations;
namespace FirstUI.DTO;

public class CreateLightconeDTO
{
        [Required(ErrorMessage = "Numele este obligatoriu.")] // obligatoriu
        [StringLength(50, ErrorMessage = "Numele nu poate depăși 50 de caractere.")]
        public string Name { get; set; } = null!;

        [Range(3, 5, ErrorMessage = "Rarity trb intre 3 si 5.")]
        public int Rarity { get; set; }
        public int BaseAtk { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Selectează un Path.")]
        public int PathId {get; set; }
        }