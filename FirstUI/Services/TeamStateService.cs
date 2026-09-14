using FirstUI.DTO;
using System.Text.Json;
using Microsoft.JSInterop;
namespace FirstUI.Services;

public class TeamStateService
{
    private readonly IJSRuntime _js;
    private const string TeamStateKey = "teamState";

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

    public TeamStateService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SaveStateAsync()
    {
        var json = JsonSerializer.Serialize(Slots);
        // localStorage.setItem(cheie, valoare)
        await _js.InvokeVoidAsync("localStorage.setItem", TeamStateKey, json);
    }

    public async Task LoadStateAsync()
    {
        var json = await _js.InvokeAsync<string>("localStorage.getItem", TeamStateKey);
        if (!string.IsNullOrEmpty(json))
        {
            var loadedSlots = JsonSerializer.Deserialize<TeamMember[]>(json);
            if (loadedSlots != null && loadedSlots.Length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    Slots[i] = loadedSlots[i];
                }
                NotifyStateChanged();
            }
        }
    }

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
        _ = SaveStateAsync(); // Salvăm starea echipei după adăugarea personajului
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
        _ = SaveStateAsync(); // Salvăm starea echipei după echiparea lightcone-ului
        return true;
    }  

    public void RemoveCharacter(int slotIndex)
    {
        Slots[slotIndex].Character = null;
        Slots[slotIndex].EquippedLightcone = null; // Daca stergem personajul, stergem si lightcone-ul
        NotifyStateChanged();
        _ = SaveStateAsync();
    }

    public void RemoveLightcone(int slotIndex)
    {
        Slots[slotIndex].EquippedLightcone = null;
        NotifyStateChanged();
        _ = SaveStateAsync();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    public int TotalTeamAtk => Slots
    .Where (s => s.EquippedLightcone != null)
    .Sum(s => s.EquippedLightcone!.BaseAtk);

    public int ActiveMembersCount => Slots.Count(s => s.Character != null);
}