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
            commandRegex = new Regex(@"^ask (.+)( ?about ?| ?: ?)(.+)$", RegexOptions.IgnoreCase);
        }

        public override bool IsCombatCommand(Character character = null)
        {
            return false;
        }

        public override bool IsWorkbenchCommand(Character character = null)
        {
            return false;
        }

        public override bool IsTradeCommand(Character character = null)
        {
            return false;
        }

        public override bool IsSearchCommand(Character character = null)
        {
            return false;
        }

        public override void Run(Character character, Room room)
        {
            if (character.sleeping)
            {
                character.Message("You are asleep.");
                return;
            }

            if (!character.ActionCooldownOver())
            {
                return;
            }

            bool direct = character is NPC;
            var targetName = GetArgument(1);
            var knowledge = GetArgument(3);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                character.Message("Who are you asking?");
                return;
            }

            var target = room.FindInRoom(targetName) as NPC;
            if (target == null)
            {
                character.Message($@"Can't find any ""{targetName}"" here.");
                return;
            }

            if (String.IsNullOrWhiteSpace(knowledge))
            {
                character.Message("What are you asking?");
                return;
            }
            var response = target.GetKnowledgeResponse(knowledge);
            if (response == null)
            {
                character.Message($"You can't ask {target.GetName()}");
                return;
            }
            character.Message($"**{target.GetName()}** (about \"*{knowledge}*\")\n{response}");

            character.RewardExperience(1);
        }
    }
}
