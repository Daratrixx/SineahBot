using SineahBot.Data;
using SineahBot.Data.Enums;
using SineahBot.Extensions;
using System;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaAlteration : Command
    {
        public CommandMetaAlteration()
        {
            commandRegex = new Regex(@"^!(alterations ?|alt ?)(.*)$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            var className = GetArgument(2);

            if (string.IsNullOrWhiteSpace(className))
            {
                character.Message("Type `!alterations [alteration name]` to get some details about the specified alteration.");
                return;
            }

            AlterationType alterationType;

            if (!Enum.TryParse(className, true, out alterationType))
            {
                character.Message($@"Unknown alteration ""{className}"".");
                return;
            }

            character.Message($"**ALTERATION** - *{alterationType}*\n> *{alterationType.GetDescription()}*");
        }
    }
}
