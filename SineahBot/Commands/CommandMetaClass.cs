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
    }
}
