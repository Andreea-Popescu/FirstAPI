namespace FirstAPI.DTO;

public class PlanetResponseDTO {

    public string Name { get; set; } = null!;
    public string Description {get; set; } = null!;
}

// Essentially, folosim un DTO pt planete ca sa evitam afisarea caracterelor dp planete cand rulam API-ul de afisare detaliata caracter
// Basically exact codu pt Planet model dar fara lista de caractere (aka aici avem ce vrem sa se vada)