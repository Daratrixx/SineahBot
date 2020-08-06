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

        public abstract void Run(IAgent agent, Room room = null);

    }
}
