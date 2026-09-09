namespace FirstAPI.models
{
    
    public class Aeon
    {
        public int Id {get; set;}

        public string Name {get; set;} = null!;

        public string Description {get; set;} = null!;

        public int PathId {get; set;}

        public Path? Path {get; set;}

        public string? ImageUrl {get; set; }
    }
}