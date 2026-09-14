namespace FirstUI.DTO
{
    public class TeamMember
    {
        public int SlotIndex { get; set; }
        public CharacterResponseDTO? Character { get; set; }
        public LightconeResponseDTO? EquippedLightcone { get; set; } 
    }
}