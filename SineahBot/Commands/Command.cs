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

        public bool IsMessageMatchingCommand(string message) {
            commandMatch = commandRegex.Match(message);
            return commandMatch != null && commandMatch.Success;
        }

        protected string GetArgument(int argumentGroupIndex) {
            return commandMatch.Groups[argumentGroupIndex].Value?.Trim();
        }

        public abstract void Run(IAgent agent, Room room = null);
        public virtual bool IsNormalCommand(IAgent agent = null)
        {
            return true;
        }
        public virtual bool IsCombatCommand(IAgent agent = null)
        {
            return true;
        }
        public virtual bool IsWorkbenchCommand(IAgent agent = null)
        {
            return true;
        }
    }
}
