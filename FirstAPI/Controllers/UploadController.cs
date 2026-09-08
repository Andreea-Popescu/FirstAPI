using Microsoft.AspNetCore.Mvc;
namespace FirstAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public UploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

// POST pt upload fisiere api/upload/character-image
[HttpPost("character-image")]
public async Task <IActionResult> UploadCharacterImage ([FromForm] IFormFile file)
    {
        // verificam sa nu fie gol fisierul
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nu a fost selectat niciun fișier.");
        }

        // Setam extensiile si verificam extensia fisierului
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest("Tip de fișier nepermis! Sunt acceptate doar JPG, PNG și WEBP.");
        }

        var uniqueFileName = $"{Guid.NewGuid()}{extension}"; // dam un nume unic fisierului incarcat
        var uploadsFolder = Path.Combine(_environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "images", "characters");
        // spunem sa salveze fisieru pe calea wwwroot/images/characters
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder); // dc nu exista, sa l creeze
        }
        var filePath = Path.Combine(uploadsFolder, uniqueFileName); // aici pune fisieru in folder

        using (var stream = new FileStream(filePath, FileMode.Create)) // scrierea binara
        {
            await file.CopyToAsync(stream);
        }

        // returnam calea relativa spre fisier
        var relativeUrl = $"/images/characters/{uniqueFileName}";
        return Ok(new { url = relativeUrl });
    }
}