using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandSay : Command
    {

        public CommandSay()
        {
            commandRegex = new Regex(@"^(talk |say |t |"" ?)(.+)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            var speach = GetArgument(2);

            if (String.IsNullOrWhiteSpace(speach))
            {
                agent.Message("What are you trying to say ?");
            }
            else
            {
                room.DescribeAction($@"**{agent.GetName()}** said: ""{speach}""", agent);
                agent.Message($@"You said: ""{speach}""");
                if (agent is Character) (agent as Character).experience += 1;
            }
        }

    }
}
