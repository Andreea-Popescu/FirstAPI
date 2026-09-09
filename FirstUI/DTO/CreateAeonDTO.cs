using System.ComponentModel.DataAnnotations;
namespace FirstUI.DTO;

public class CreateAeonDTO
{
    [Required(ErrorMessage = "Numele este obligatoriu.")] // obligatoriu
    [StringLength(50, ErrorMessage = "Numele nu poate depăși 50 de caractere.")]
    public string Name { get; set; } = null!; 

    [StringLength(4000, ErrorMessage = "Descrierea nu poate depasi 4000 de caractere.")]
    public string Description {get; set;} = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Selectează un Path.")]
    public int PathId { get; set; }

    public string? ImageUrl { get; set; }
}


// model pt formular