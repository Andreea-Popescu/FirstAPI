using System.ComponentModel.DataAnnotations;
namespace FirstUI.DTO;

public class CreateCharacterDTO
{
    [Required(ErrorMessage = "Numele este obligatoriu.")] // obligatoriu
    [StringLength(50, ErrorMessage = "Numele nu poate depăși 50 de caractere.")]
    public string Name { get; set; } = null!; // sau string.empty

    [Range(1, 100, ErrorMessage = "Rating-ul trebuie să fie între 1 și 100.")]
    public int Rating { get; set; } = 50;

    [Range(1, int.MaxValue, ErrorMessage = "Selectează o planetă.")]
    public int PlanetId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selectează un element.")]
    public int ElementId { get; set; }
}

// model pt formular