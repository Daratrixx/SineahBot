using System;

namespace SineahBot.Data
{
    public class CharacterMessage
    {
        public Guid id { get; set; }
        public Guid idCharacter { get; set; }
        public string idRoom { get; set; }
        public string message { get; set; }
    }
}
