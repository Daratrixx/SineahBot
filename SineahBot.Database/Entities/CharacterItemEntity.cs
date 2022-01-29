namespace SineahBot.Database.Entities
{
    public class CharacterItemEntity : CharacterEntityBase
    {
        public string ItemName { get; set; }
        public int StackSize { get; set; }
    }
}
