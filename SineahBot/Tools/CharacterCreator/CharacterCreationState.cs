using SineahBot.Data;
using SineahBot.Data.Enums;

namespace SineahBot.Tools
{
    public static partial class CharacterCreator
    {
        public class CharacterCreationState
        {
            public Player Player;
            public int CurrentStep = 0;

            public string Name { get; set; }
            public string Gender { get; set; }
            public string Pronouns { get; set; }
            public CharacterClass CharacterClass { get; set; }
            public CharacterAncestry CharacterAncestry { get; set; } = CharacterAncestry.Human;
        }
    }
}
