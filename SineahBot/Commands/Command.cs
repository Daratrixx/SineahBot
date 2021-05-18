using SineahBot.Data;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public abstract class Command
    {
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

        public abstract void Run(Character character, Room room = null);

        public virtual bool CanUseCommand(Character character)
        {
            switch (character.characterStatus)
            {
                case CharacterStatus.Normal: return isNormalCommand;
                case CharacterStatus.Combat: return isCombatCommand;
                case CharacterStatus.Trade: return isTradeCommand;
                case CharacterStatus.Search: return isSearchCommand;
                default: return false;
            }
        }
    }
}
