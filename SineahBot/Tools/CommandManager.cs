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
        public static void ParseUserMessage(string userId, string message)
        {
            var player = PlayerManager.GetPlayer(userId);
            switch (player.playerStatus)
            {
                case PlayerStatus.InCharacter:
                    var character = player.character;
                    if (character.currentRoomId == Guid.Empty)
                        character.currentRoomId = RoomManager.GetSpawnRoomId();

                    var room = RoomManager.GetRoom(character.currentRoomId);
                    ParseInCharacterMessage(character, message, room);
                    break;
                case PlayerStatus.OutCharacter:
                    ParseOutCharacterMessage(player, message);
                    break;
                case PlayerStatus.CharacterNaming:
                    ParseNoCharacterMessage(player, message);
                    break;
                case PlayerStatus.CharacterClassSelection:
                    ParseNoCharacterMessage(player, message);
                    break;
                case PlayerStatus.None:
                    ParseNoCharacterMessage(player, message);
                    break;
            }
        }

        public static void ParseNoCharacterMessage(IAgent agent, string command)
        {
            NoCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }
        public static void ParseInCharacterMessage(IAgent agent, string command, Room room)
        {
            InCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent, room);
        }
        public static void ParseOutCharacterMessage(IAgent agent, string command)
        {
            OutCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }


        public static List<Command> NoCharacterCommands = new List<Command>() { };
        public static List<Command> InCharacterCommands = new List<Command>() { new CommandMove() };
        public static List<Command> OutCharacterCommands = new List<Command>() { };

    }
}
