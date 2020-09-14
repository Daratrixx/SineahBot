﻿using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandLevel : Command
    {

        public CommandLevel()
        {
            commandRegex = new Regex(@"^(!level)( .+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            var className = GetArgument(2);
            if (character.experience >= ClassProgressionManager.ExperienceForNextLevel(character.level))
            {
                var subclasses = ClassProgressionManager.GetAvailableClassChange(character);
                if (subclasses.Length > 0)
                {
                    if (className == "")
                    {
                        character.Message($"You have to indicate what subclass you wish to level up as [**!level** {subclasses.Select(x => x.Key.ToString()).Aggregate((a, b) => a + "/" + b)}]\n> To keep your current class, type **!level {character.characterClass}**");
                    }
                    else
                    {
                        CharacterClass targetClass;
                        if (!Enum.TryParse(className, true, out targetClass))
                        {
                            character.Message($"\"className\" is not a valid class name. Possible values are [{subclasses.Select(x => x.Key.ToString()).Aggregate((a, b) => a + "/" + b)}]\n> To keep your current class, type **!level {character.characterClass}**");
                        }
                        else
                        {
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
                    }
                }
                else
                {
                    if (className != "")
                    {
                        character.Message("You can't level up with an alternative class. Type **!level** to level up normally.");
                    }
                    else
                    {
                        LevelUpCharacterAs(character, character.characterClass);
                    }
                }
            }
            else
            {
                character.Message($"You don't have enough experience to level up ({character.experience}/{ClassProgressionManager.ExperienceForNextLevel(character.level)})");
            }
        }

        public void LevelUpCharacterAs(Character character, CharacterClass targetClass)
        {
            character.characterClass = targetClass;
            character.experience -= ClassProgressionManager.ExperienceForNextLevel(character.level);
            character.level += 1;
            ClassProgressionManager.ApplyClassProgressionForCharacter(character, true);
            character.Message($"You are now level {character.level} {character.characterClass}! You will need a total of {ClassProgressionManager.ExperienceForNextLevel(character.level)} experience to reach the next level.");
        }

    }
}
