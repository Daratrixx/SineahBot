namespace SineahBot.Database.Entities
{
    public class CharacterMessageEntity : CharacterEntityBase
    {
        public string IdRoom { get; set; }
        public string Message { get; set; }
    }
}
