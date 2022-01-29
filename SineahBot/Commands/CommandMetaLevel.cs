using SineahBot.Data;
using SineahBot.Tools;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaLevel : Command
    {
        public CommandMetaLevel()
        {
            commandRegex = new Regex(@"^(!level)( .+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            var className = GetArgument(2);

            if (character.experience < ClassProgressionManager.ExperienceForNextLevel(character.level))
            {
                character.Message($"You don't have enough experience to level up ({character.experience}/{ClassProgressionManager.ExperienceForNextLevel(character.level)})");
                return;
            }

            var subclasses = ClassProgressionManager.GetAvailableClassChange(character);
            if (subclasses.Length > 0)
            {
                if (className == "")
                {
                    character.Message($"You have to indicate what subclass you wish to level up as [**!level** {subclasses.Select(x => x.Key.ToString()).Aggregate((a, b) => a + "/" + b)}]" +
                    $"\n> To keep your current class, type **!level {character.characterClass}**" +
                    $"\n> Type `!class [class name]` to get the description of a character class.");
                    return;
                }

                CharacterClass targetClass;

                if (!Enum.TryParse(className, true, out targetClass))
                {
                    character.Message($"\"className\" is not a valid class name. Possible values are [{subclasses.Select(x => x.Key.ToString()).Aggregate((a, b) => a + "/" + b)}]\n> To keep your current class, type **!level {character.characterClass}**");
                    return;
                }

                if (targetClass == character.characterClass)
                {
                    LevelUpCharacterAs(character, character.characterClass);
                }
                else if (subclasses.Select(x => x.Key).Contains(targetClass))
                {
                    LevelUpCharacterAs(character, targetClass);
                }
                else
                {
                    character.Message($"\"targetClass\" is not a valid class name. Possible values are [{subclasses.Select(x => x.Key.ToString()).Aggregate((a, b) => a + "/" + b)}]\n> To keep your current class, type **!level {character.characterClass}**");
                }
            }
            else
            {
                if (className != "")
                {
                    character.Message("You can't level up with an alternative class. Type **!level** to level up normally.");
                    return;
                }

                LevelUpCharacterAs(character, character.characterClass);
            }
        }

        public void LevelUpCharacterAs(Character character, CharacterClass targetClass)
        {
            character.characterClass = targetClass;
            character.experience -= ClassProgressionManager.ExperienceForNextLevel(character.level);
            character.level += 1;
            ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
            character.Message($"You are now a level {character.level} {character.characterClass}! You will need a total of {ClassProgressionManager.ExperienceForNextLevel(character.level)} experience to reach the next level.");
        }
    }
}
