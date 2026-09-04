namespace FirstAPI.models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int PlanetId { get; set; } // Cheia externa pt cheia primara Id din Planeta, aparent EF recunoaste ca e cheia externa
                                            // pt Planet din faptu ca se potrivesc la nume (wow) - basically asta folosim pt join
        public Planet? Planet { get; set; } // NU E COLOANA IN BAZA DE DATE!!!!
                                            // nu exista coloana numita Planet, e un obiect complet si mi permite sa fac query mai usor
                                            // EX: în loc să iei character.PlanetId 
                                            // și să faci un al doilea query manual în baza de date: _context.Planets.Find(character.PlanetId)
                                            // Semnul ? -> proprietatea poate fi nullable (spre ex, caracterul nu are o planeta)

        public int Rating { get; set; }
    }
}

// USEFUL: Vede proprietatea de navigare Planet.
// Caută automat o proprietate numită [NumeleProprietății] + Id → adică PlanetId.
// Când le găsește pereche, deduce singur: „Am înțeles! PlanetId este Foreign Key-ul din tabel, 
// iar când programatorul cere .Include(c => c.Planet), eu voi face automat un LEFT JOIN pe tabela Planets unde Characters.PlanetId == Planets.Id 
// și voi popula acest obiect.”