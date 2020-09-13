using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaHelp : Command
    {

        public CommandMetaHelp()
        {
            commandRegex = new Regex(@"^(!help)( .+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            //var entity = agent as Entity;
            var targetName = GetArgument(2);

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
HELP
> - Type **l**ook to get a description of your surroundings, or **l**ook **[object name]** to have a better description of the object.
> - Type **dir**ection to get a list of directions you can go to from where you are.
> - Type move/go **[direction]** to move in the specified direction.
> - Type **g**et **[item name]** to pick up the specified item. It will then be in your inventory.
> - Type **d**rop **[item name]** to drop the specified item from your inventory.
> - Type **lock** **[direction]** to lock the access in the specified direction. Some access might require an item to lock them.
> - Type **unlock** **[direction]** to unlock the access in the specified direction. Some access might require an item to unlock them.
> - Type **attack** **[direction]** to attack the designated character or object.
> - Type **cast [spell name] on [target name]** to cast the specified spell on the specified target.
> - Type **cast [spell name]** to cast the specified spell without a target.
> - Type !**i**nformation to get some information about your character.
> - Type !**spells** to get the list of spells you can cast.
> - **[SOON]** Type **!help [full command]** to have a detailed description of the specified command.
";

    }
}
