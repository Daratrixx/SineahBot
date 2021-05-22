using SineahBot.Data;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public abstract class Command
    {
        protected static readonly string targetRegex_3 = @"((.+?)( \d+)?)";

        protected Regex commandRegex;
        protected Match commandMatch;

        protected bool isNormalCommand = true;
        protected bool isCombatCommand = true;
        protected bool isTradeCommand = true;
        protected bool isSearchCommand = true;

        public bool IsMessageMatchingCommand(string message)
        {
            commandMatch = commandRegex.Match(message);
            return commandMatch != null && commandMatch.Success;
        }

        protected string GetArgument(int argumentGroupIndex)
        {
            if (argumentGroupIndex >= commandMatch.Groups.Count) return null;
            return commandMatch.Groups[argumentGroupIndex].Value?.Trim();
        }

        protected bool HasArgument(int argumentGroupIndex)
        {
            if (argumentGroupIndex >= commandMatch.Groups.Count) return false;
            return !string.IsNullOrWhiteSpace(commandMatch.Groups[argumentGroupIndex].Value);
        }

        public abstract void Run(Character character, Room room = null);

        public virtual bool CanUseCommand(Character character)
        {
            if (character.HasAlteration(AlterationType.Stunned)) return false; // can't act while stunned
            if (character.sleeping) return false; // can't act while sleeping
            switch (character.characterStatus)
            {
                case CharacterStatus.Normal: return isNormalCommand;
                case CharacterStatus.Combat: return isCombatCommand;
                case CharacterStatus.Trade: return isTradeCommand;
                case CharacterStatus.Search: return isSearchCommand;
                default: return false;
            }
        }

        public TargetType GetTarget<TargetType>(Character character, Room room, int targetRegexIndex, bool messages = true) where TargetType : Entity
        {
            var targetName = GetArgument(targetRegexIndex + 1);
            if (String.IsNullOrWhiteSpace(targetName))
            {
                if (messages) character.Message("What are you trying to search?");
                return null;
            }

            var targets = room.FindAllInRoom<TargetType>(targetName);
            if (targets.Count() == 0)
            {
                if (messages) character.Message($"There is no \"{targetName}\" to search in this room.");
                return null;
            }
            if (HasArgument(targetRegexIndex + 2) && int.TryParse(GetArgument(targetRegexIndex + 2), out int targetIndex))
            {
                if (targetIndex > targets.Count()) return targets.Last();
                if (targetIndex < 0) return targets.First();
                return targets.ElementAt(targetIndex - 1);
            }
            return targets.First();
        }
    }
}
