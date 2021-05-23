using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandAsk : Command
    {
        public CommandAsk()
        {
            commandRegex = new Regex($@"^ask {targetRegex_3}( about (.+))?$", RegexOptions.IgnoreCase);
            isNormalCommand = true;
            isCombatCommand = false;
            isTradeCommand = false;
            isSearchCommand = false;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            var targetName = GetArgument(1);
            var knowledge = GetArgument(5);

            var target = GetTarget<NPC>(character, room, 1);
            if (target == null) return; // error message already given in GetTarget

            if (String.IsNullOrWhiteSpace(knowledge))
            {
                character.Message($"What do you want to ask?\n> Type `ask {targetName} about [subject]`.");
                return;
            }
            var response = target.GetKnowledgeResponse(knowledge);
            if (response == null)
            {
                character.Message($"You can't ask {target.GetName()}");
                return;
            }
            character.Message($"> **{target.GetName()}** (about \"*{knowledge}*\")\n> {response}");

            character.RewardExperience(1);
        }
    }
}
