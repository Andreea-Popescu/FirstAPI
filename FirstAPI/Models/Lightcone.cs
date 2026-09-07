namespace FirstAPI.models
{
    public class Lightcone
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Rarity {get; set;}
        public int BaseAtk {get; set; }
        public int PathId {get; set;}
        public Path? Path {get; set;}

    }
}