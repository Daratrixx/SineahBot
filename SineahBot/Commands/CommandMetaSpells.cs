using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaSpells : Command
    {

        public CommandMetaSpells()
        {
            commandRegex = new Regex(@"^(!spells)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is Entity)) throw new Exception($@"Impossible to get information as non-entity agent.");
            var targetName = commandMatch.Groups[2].Value?.Trim().ToLower();

            if (agent is Player)
            {
                var player = agent as Player;
                var character = player.character;
                agent.Message(GetCharacterSpells(character));
            }
            else if (agent is Character)
            {
                var character = agent as Character;
                agent.Message(GetCharacterSpells(character));
            }
            else
            {
                throw new Exception("Unsupported agent type, can't display spells.");
            }
        }

        public string GetCharacterSpells(Character character)
        {
            return "SPELLS\n"
            + String.Join('\n', character.spells.Select(x => GetSpellInformation(x)))
            + "\n *Type **!spells [spell name]** to get more informations about the specified spell.*";
        }
        public string GetSpellInformation(Spell spell)
        {
            return $"> {spell.GetName()} - {spell.description}";
        }

    }
}
