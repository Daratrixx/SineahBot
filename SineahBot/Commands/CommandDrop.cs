using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandDrop : Command
    {

        public CommandDrop()
        {
            commandRegex = new Regex(@"^(drop |d )(.+)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is IInventory)) throw new Exception($@"Impossible to drop items as non-inventory agent");
            //var entity = agent as Entity;
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                agent.Message("What are you trying to drop ?");
            }
            else
            {
                var inventory = agent as IInventory;
                var target = room.FindInRoom(targetName);
                if (target == null && (agent is IInventory)) target = (agent as IInventory).FindInInventory(targetName);
                if (target != null && target is Item)
                {
                    var itemTarget = target as Item;
                    inventory.RemoveFromInventory(itemTarget);
                    agent.Message($"You dropped {itemTarget.name}.");
                    if (agent is Entity)
                        room.DescribeAction($"{(agent as Entity).name} dropped {itemTarget.name}.", agent);
                    room.AddToRoom(itemTarget);
                    if (agent is Character) (agent as Character).experience += 1;
                }
                else
                {
                    agent.Message($@"Can't find any ""{targetName}""!");
                }
            }
        }

        public override bool IsWorkbenchCommand(IAgent agent = null)
        {
            return false;
        }

    }
}
