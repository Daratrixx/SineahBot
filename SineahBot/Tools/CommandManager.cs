using SineahBot.Commands;
using SineahBot.Data;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;

namespace SineahBot.Tools
{
    public static class CommandManager
    {
        public static void ParseUserMessage(ulong userId, string message, ulong? channelId = null)
        {
            if(message == "!stop" && userId == 109406259643437056)
            {
                Program.DiscordClient.StopAsync();
                System.Environment.Exit(0);
            }
            var player = PlayerManager.GetPlayer(userId);
            if (channelId.HasValue) player.channelId = channelId.Value;
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
                case PlayerStatus.CharacterCreation:
                case PlayerStatus.None:
                    ParseCharacterCreationMessage(player, message);
                    break;
            }
        }

        public static void ParseNoCharacterMessage(IAgent agent, string command)
        {
            NoCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }
        public static void ParseCharacterCreationMessage(Player player, string command)
        {
            switch (player.playerCharacterCreationStatus)
            {
                case PlayerCharacterCreationStatus.None:
                    player.Message("You are about to start your adventure. What is your **name**, mortal ?");
                    player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Naming;
                    break;
                case PlayerCharacterCreationStatus.Naming:
                    player.characterName = command;
                    player.Message($@"""{command}""... Is this how youi will be called from now on ? [**y**es/**n**o]");
                    player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.NamingConfirmation;
                    break;
                case PlayerCharacterCreationStatus.NamingConfirmation:
                    if (command == "yes" || command == "y")
                    {
                        player.Message($@"You are now ready to walk the world. Type **!help** to learn how to play. Farewell for now, mortal.");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.NamingConfirmation;
                        var character = CharacterManager.CreateCharacterForPlayer(player);
                        character.currentRoomId = RoomManager.GetSpawnRoomId();
                        RoomManager.GetRoom(character.currentRoomId).AddToRoom(character);
                        player.playerStatus = PlayerStatus.InCharacter;
                    }
                    else if (command == "no" || command == "n")
                    {
                        player.Message("What is your **name**, mortal ?");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Naming;
                    }
                    else
                    {
                        player.Message($@"Type **yes** to confirm that ""{player.characterName}"" will be your name, or **no** to enter a new name.");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Naming;
                    }
                    break;
            }
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
        public static List<Command> InCharacterCommands = new List<Command>() { new CommandMove(), new CommandLook(), new CommandPickup(), new CommandDrop() };
        public static List<Command> OutCharacterCommands = new List<Command>() { };
    }
}
