using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandInformation : Command
    {

        public CommandInformation()
        {
            commandRegex = new Regex(@"^(i|info|self|information)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is Entity)) throw new Exception($@"Impossible to get information as non-entity agent");
            if (agent is Player)
            {
                var player = agent as Player;
                var character = player.character;
                agent.Message($@"
> INFORMATION
> Health : {character.health}/{character.maxHealth}
");

            }
            else if (agent is Character)
            {
                var character = agent as Character;
                agent.Message($@"
> INFORMATION
> Health : {character.health}/{character.maxHealth}
");
            }
            else
            {

            }
        }

    }
}
