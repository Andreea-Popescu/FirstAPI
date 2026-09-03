namespace FirstAPI.models
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<Character> Characters { get; set; } = new(); // o planeta poate avea mai multe (lista) personaje: one-to-many 

    }
}