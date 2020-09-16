using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaClass : Command
    {

        public CommandMetaClass()
        {
            commandRegex = new Regex(@"^!(class ?|c ?)(.*)$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            var className = GetArgument(2);

            if (string.IsNullOrWhiteSpace(className))
            {
                character.Message("Type **!class [class name]** to get some details about the specified class.");
                return;
            }

            CharacterClass characterClass;

            if (!Enum.TryParse(className, true, out characterClass))
            {
                character.Message($@"Unknown class ""{className}"".");
                return;
            }

            character.Message($"**CLASS** - *{characterClass}*\n> *{ClassProgressionManager.GetClassDescription(characterClass)}*");
        }

        public string GetCharacterInformation(Character character)
        {
            return $@"
**INFORMATION** - ***{character.name}***
> *{character.characterClass.ToString().ToUpper()}* *level **{character.level}*** (*{character.experience}/{ClassProgressionManager.ExperienceForNextLevel(character.level)} exp*)
> *Health* : **{character.health}/{character.maxHealth}**
> *Mana* : **{character.mana}/{character.maxMana}**
> *Spell power* : **{character.GetSpellPower()}** ({(ClassProgressionManager.IsMagicalClass(character.characterClass) ? "2" : "1")} x level)
> *Physical power* : **{character.GetWeaponDamage()}** ({(ClassProgressionManager.IsPhysicalClass(character.characterClass) ? "2" : "1")} x level)
{(character.experience >= ClassProgressionManager.ExperienceForNextLevel(character.level) ? "*You have enough experience to level up! Type **!level***" : "")}
";
        }
    }
}
