﻿using SineahBot.Commands;
using SineahBot.Data;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Tools
{
    public static class CommandManager
    {
        private static Regex createPrivateChannelRegex = new Regex(@"!create (\d+) (\d+)");
        public static void ParseUserMessage(ulong userId, string message, ulong? channelId = null)
        {
            var player = PlayerManager.GetPlayer(userId);
            if (message.StartsWith("!") && userId == 109406259643437056)
            {
                if (userId == 109406259643437056)
                {
                    var create = createPrivateChannelRegex.Match(message);
                    if (create.Success)
                    {
                        ulong createGuildId = ulong.Parse(create.Groups[1].Value);
                        ulong createUserId = ulong.Parse(create.Groups[2].Value);
                        Program.CreatePrivateChannel(createGuildId, createUserId);
                        return;
                    }
                    if (message == "!save")
                    {
                        Program.SaveData();
                        return;
                    }
                    if (message == "!stop")
                    {
                        Program.DiscordClient.StopAsync();
                        Program.SaveData();
                        Environment.Exit(0);
                    }
                    if (message == "!boost" && player.character != null)
                    {
                        var exp = ClassProgressionManager.ExperienceForNextLevel(player.character.level);
                        player.character.experience += exp;
                        player.Message($"Earned {exp} experience.");
                        return;
                    }
                    if (message == "!boosts")
                    {
                        foreach (var c in Program.database.Characters)
                        {
                            var exp = ClassProgressionManager.ExperienceForNextLevel(c.level);
                            c.experience += exp;
                        }
                        return;
                    }
                }
                var m2 = message.ToLower();
                if (message == "!disconnect" && player.character != null && player.character.currentRoomId != Guid.Empty)
                {
                    // set the disconnection timer to 0 minutes before alert
                    player.SetDisconnectTimer(0);
                    return;
                }
            }
            if (channelId.HasValue) player.channelId = channelId.Value;
            if (ParseMetaCommand(player, message)) return;
            switch (player.playerStatus)
            {
                case PlayerStatus.InCharacter:
                    Room room = null;
                    var character = player.character;
                    if (character.currentRoomId == Guid.Empty)
                    {
                        room = RoomManager.GetRoom(RoomManager.GetSpawnRoomId());
                        room.AddToRoom(character, false); // add the player character to room but don't show it to the player
                    }
                    else
                    {
                        room = RoomManager.GetRoom(character.currentRoomId);
                    }
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
            // reset the afk disconnection timer to "10 minutes before alert"
            player.SetDisconnectTimer(10);
        }

        public static bool ParseMetaCommand(IAgent agent, string command)
        {
            var metaCommand = MetaCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command));
            if (metaCommand == null) return false;
            var character = (agent as Player)?.character;
            if (character != null)
            {
                metaCommand.Run(character);
            }
            else
            {
                agent.Message("Impossible to run this command without a character.");
            }
            return true;
        }

        public static void ParseNoCharacterMessage(IAgent agent, string command)
        {
            //NoCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }
        public static void ParseCharacterCreationMessage(Player player, string command)
        {
            switch (player.playerCharacterCreationStatus)
            {
                case PlayerCharacterCreationStatus.None:
                    player.Message("You are about to start your adventure.\n> What is your **name**, mortal ?");
                    player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Naming;
                    break;
                case PlayerCharacterCreationStatus.Naming:
                    player.characterName = command;
                    player.Message($@"""{command}""... Is this how you will be called from now on ? [**y**es/**n**o]");
                    player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.NamingConfirmation;
                    break;
                case PlayerCharacterCreationStatus.NamingConfirmation:
                    command = command.ToLower();
                    if (command == "yes" || command == "y" || command == "yep" || command == "affirmative")
                    {
                        player.Message($@"""{player.characterName}"" will now be your name in this world.
> What will be your starting class ? [{ClassProgressionManager.GetStartClassListString()}]");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Classing;
                    }
                    else if (command == "no" || command == "n" || command == "nope" || command == "negative")
                    {
                        player.Message("What is your **name**, mortal ?");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Naming;
                    }
                    else
                    {
                        player.Message($@"Type **yes** to confirm that ""{player.characterName}"" will be your name, or **no** to enter a new name.");
                    }
                    break;
                case PlayerCharacterCreationStatus.Classing:
                    if (Enum.TryParse(command, true, out player.characterClass) && ClassProgressionManager.IsStartingClass(player.characterClass))
                    {
                        player.Message($@"""{player.characterClass}""... will this be your starting class, your initial role in this world ? [**y**es/**n**o]
> **{player.characterClass}**: *{ClassProgressionManager.GetClassDescription(player.characterClass)}*");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.ClassingConfirmation;
                    }
                    else
                    {
                        player.Message($@"""{command}"" is not a recognized starting class. Type one of the following : [{ClassProgressionManager.GetStartClassListString()}]");
                    }
                    break;
                case PlayerCharacterCreationStatus.ClassingConfirmation:
                    command = command.ToLower();
                    if (command == "yes" || command == "y" || command == "yep" || command == "affirmative")
                    {
                        FinishCharacterCreation(player);
                    }
                    else if (command == "no" || command == "n" || command == "nope" || command == "negative")
                    {
                        player.Message("What is your **class**, mortal ?");
                        player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.Classing;
                    }
                    else
                    {
                        player.Message($@"Type **yes** to confirm that ""{player.characterClass}"" will be your class, or **no** to choose a new class.");
                    }
                    break;
            }
        }
        public static void FinishCharacterCreation(Player player)
        {
            player.Message($@"You are now ready to walk the world. Type **!help** to learn how to play. Farewell for now, mortal.");
            player.playerCharacterCreationStatus = PlayerCharacterCreationStatus.None;
            var character = CharacterManager.CreateCharacterForPlayer(player);
            character.currentRoomId = RoomManager.GetSpawnRoomId();
            RoomManager.GetRoom(character.currentRoomId).AddToRoom(character);
            player.playerStatus = PlayerStatus.InCharacter;
        }
        public static void ParseInCharacterMessage(Character character, string command, Room room)
        {
            var characterStatus = character.characterStatus;
            switch (characterStatus)
            {
                case CharacterStatus.Normal:
                    InCharacterCommands.Where(x => x.IsNormalCommand(character)).FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(character, room);
                    break;
                case CharacterStatus.Combat:
                    InCharacterCommands.Where(x => x.IsCombatCommand(character)).FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(character, room);
                    break;
                case CharacterStatus.Workbench:
                    InCharacterCommands.Where(x => x.IsWorkbenchCommand(character)).FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(character, room);
                    break;
                case CharacterStatus.Trade:
                    InCharacterCommands.Where(x => x.IsTradeCommand(character)).FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(character, room);
                    break;
                default:
                    throw new Exception($"Impossible to parse an in-character command for a character in the unsupported character state : {characterStatus}");
            }
        }
        public static void ParseOutCharacterMessage(IAgent agent, string command)
        {
            //OutCharacterCommands.FirstOrDefault(x => x.IsMessageMatchingCommand(command))?.Run(agent);
        }

        public static List<Command> MetaCommands = new List<Command>() {
            new CommandMetaInformation(), new CommandMetaInventory(),
            new CommandMetaLevel(),
            new CommandMetaSpells(), new CommandMetaClass(), new CommandMetaAlteration(),
            new CommandMetaHelp() };
        public static List<Command> NoCharacterCommands = new List<Command>() { };
        public static List<Command> InCharacterCommands = new List<Command>() {
            new CommandMove(), new CommandLook(), new CommandDirection(),
            new CommandPickup(), new CommandDrop(), new CommandConsume(),
            new CommandLock(), new CommandUnlock(),
            new CommandSay(), new CommandAct(),
            new CommandCombatAttack(), new CommandCombatFlee(),
            new CommandCastOn(), new CommandCast(),
            new CommandEquip(), new CommandUnequip(),
            new CommandSleep(), new CommandAsk(),
            new CommandTrade(), new CommandTradeList(), new CommandTradeBuy(), new CommandTradeSell(), new CommandTradeLeave(),
        };
        public static List<Command> OutCharacterCommands = new List<Command>() { };
    }
}
