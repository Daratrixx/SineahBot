using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandLook : Command
    {

        public CommandLook()
        {
            commandRegex = new Regex(@"^(look|l) ?(.+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is Entity)) throw new Exception($@"Impossible to move non-entity agent");
            //var entity = agent as Entity;
            var targetName = commandMatch.Groups[2].Value;

            if (String.IsNullOrWhiteSpace(targetName))
            {
                agent.Message(room.GetFullDescription(agent));
            }
            else
            {
                var target = room.FindInRoom(targetName);
                if (target == null && agent is IInventory) target = (agent as IInventory).FindInInventory(targetName);
                if (target != null && target is IObservable)
                {
                    var observableTarget = target as IObservable;
                    agent.Message(observableTarget.GetFullDescription(agent));
                }
                else
                {
                    agent.Message($@"Can't find any ""{targetName}"" here !");
                }
            }
        }
    }
}
