using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaDamage : Command
    {
        public CommandMetaDamage()
        {
            commandRegex = new Regex(@"^!(damage ?|dmg ?)(.*)$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {

            if (!HasArgument(2))
            {
                character.Message($"Type `!damage [damage type name]` to get some details about the specified damage type.\n**Damage types**: *{String.Join(", ", Enum.GetNames(typeof(DamageType)))}.*");
                return;
            }

            var damageTypeName = GetArgument(2);
            DamageType damageType;

            if (!Enum.TryParse(damageTypeName, true, out damageType))
            {
                character.Message($@"Unknown damage ""{damageTypeName}"".");
                return;
            }

            character.Message($"**DAMAGE TYPE** - *{damageType}*\n> *{Damage.GetDamageTypeDescription(damageType)}*");
        }
    }
}
