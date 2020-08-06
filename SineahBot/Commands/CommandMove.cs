using SineahBot.Data;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMove : Command
    {

        public CommandMove()
        {
            commandRegex = new Regex(@"^(move |go |move to |go to )?(north|n|east|e|south|s|west|w|in|out)$");
        }

        public override void Run(IAgent agent, Room room) {

        }

    }
}
