using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandHelp : Command
    {

        public CommandHelp()
        {
            commandRegex = new Regex(@"^(!help)( .+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            //var entity = agent as Entity;
            var targetName = commandMatch.Groups[2].Value?.Trim().ToLower();

            if (String.IsNullOrWhiteSpace(targetName))
            {
                agent.Message(HelpMessage);
            }
            else
            {
                agent.Message(HelpMessage);
            }
        }

        public const string HelpMessage = @"
> Basics :
> - Type **l**ook to get a description of your surroundings, or **l**ook **[object name]** to have a better description of the object.
> - Type **dir**ection to get a list of directions you can go to from where you are.
> - Type move/go **[direction]** to move in the specified direction.
> - Type **g**et **[item name]** to pick up the specified item. It will then be in your inventory.
> - Type **d**rop **[item name]** to drop the specified item from your inventory.
> - **[SOON]** Type **!help [full command]** to have a detailed description of the specified command.
";
    }
}
