namespace FirstAPI.models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Planet { get; set; } = null!;

        public int Rating { get; set; }
    }
}