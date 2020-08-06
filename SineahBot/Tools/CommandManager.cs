using SineahBot.Commands;
using SineahBot.Data;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Tools
{
    public static class CommandManager
    {
        public static void ParsePlayerMessage(string playerId, string message)
        {

        }

        public static void ParseNoCharacterMessage(IAgent agent, string command)
        {
            NoCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }
        public static void ParseInCharacterMessage(IAgent agent, Room room, string command)
        {
            InCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent, room);
        }
        public static void ParseOutCharacterMessage(IAgent agent, string command)
        {
            OutCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }


        public static List<Command> NoCharacterCommands = new List<Command>() {  };
        public static List<Command> InCharacterCommands = new List<Command>() { new CommandMove() };
        public static List<Command> OutCharacterCommands = new List<Command>() { };

    }
}
