namespace FirstUI.DTO;

public class AeonResponseDTO
{
        public int Id {get; set;}
        public string Name { get; set; } = null!;

        public string Description {get; set;} = null!;

        public string PathName {get; set;} = null!;

        public string? ImageUrl { get; set; }
}