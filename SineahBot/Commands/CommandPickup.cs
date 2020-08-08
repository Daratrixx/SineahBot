using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandPickup : Command
    {

        public CommandPickup()
        {
            commandRegex = new Regex(@"^(get|g|pickup|pick up|grab|take) (.+)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is IInventory)) throw new Exception($@"Impossible to pick-up items as non-inventory agent");
            //var entity = agent as Entity;
            var targetName = GetArgument(2);

            if (String.IsNullOrWhiteSpace(targetName))
            {
                agent.Message("What are you trying to get ?");
            }
            else
            {
                var inventory = agent as IInventory;
                var target = room.FindInRoom(targetName);
                if (target != null && target is Item)
                {
                    var itemTarget = target as Item;
                    room.RemoveFromRoom(itemTarget);
                    agent.Message($"You picked up {itemTarget.name}.");
                    if (agent is Entity)
                        room.DescribeAction($"{(agent as Entity).name} picked up {itemTarget.name}.", agent);
                    inventory.AddToInventory(itemTarget);
                    if (agent is Character) (agent as Character).experience += 1;
                }
                else
                {
                    agent.Message($@"Can't find any ""{targetName}"" here !");
                }
            }
        }
    }
}
