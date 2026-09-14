using FirstUI.DTO;
namespace FirstUI.Services;

public class TeamStateService
{
    // Echipa are fix 4 sloturi: 0,1,2,3
    public TeamMember[] Slots {get; } = new TeamMember[4]
    {
        new TeamMember { SlotIndex = 0 },
        new TeamMember { SlotIndex = 1 },
        new TeamMember { SlotIndex = 2 },
        new TeamMember { SlotIndex = 3 }
    };

    // Event pt a notifica componentele cand se schimba ceva in echipa
    public event Action? OnChange; // mecanism de notificare pentru schimbari in echipa

    public bool AddCharacterToSlot(int slotIndex, CharacterResponseDTO character, out string errorMessage)
    {
        errorMessage = string.Empty;

        // Regula 1: Verificăm dacă personajul e deja în alt slot
        if (Slots.Any(s => s.Character?.Id == character.Id))
        {
            errorMessage = "Characterul este deja într-un alt slot.";
            return false;
        }

        Slots[slotIndex].Character = character;

        NotifyStateChanged();
        return true;
    }

    public bool EquipLightcone(int slotIndex, LightconeResponseDTO lightcone, out string errorMessage)
    {
        errorMessage = string.Empty;

        var character = Slots[slotIndex].Character;
        if (character == null)
        {
            errorMessage = "Nu există un personaj în acest slot. Adauga un personaj inainte!";
            return false;
        }
        
        if (!string.Equals(character.PathName, lightcone.PathName, StringComparison.OrdinalIgnoreCase))
        {
            errorMessage = $"Incompatibilitate! {lightcone.Name} este {lightcone.PathName}, dar {character.Name} este {character.PathName}!";
            return false;
        }

        if(Slots.Any(s => s.EquippedLightcone?.Id == lightcone.Id))
        {
            errorMessage = "Lightcone-ul este deja echipat într-un alt slot.";
            return false;
        }

        Slots[slotIndex].EquippedLightcone = lightcone;
        NotifyStateChanged();
        return true;
    }  

    public void RemoveCharacter(int slotIndex)
    {
        Slots[slotIndex].Character = null;
        Slots[slotIndex].EquippedLightcone = null; // Daca stergem personajul, stergem si lightcone-ul
        NotifyStateChanged();
    }

    public void RemoveLightcone(int slotIndex)
    {
        Slots[slotIndex].EquippedLightcone = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}