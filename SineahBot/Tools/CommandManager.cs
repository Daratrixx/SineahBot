using SineahBot.Commands;
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
            if (message.StartsWith("!"))
            {
                var m2 = message.ToLower();
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
                    if (m2 == "!save")
                    {
                        Program.SaveData();
                        return;
                    }
                    if (m2 == "!stop")
                    {
                        Program.DiscordClient.StopAsync();
                        Program.SaveData();
                        Environment.Exit(0);
                    }
                    if (m2 == "!boost" && player.character != null)
                    {
                        var exp = ClassProgressionManager.ExperienceForNextLevel(player.character.level);
                        player.character.experience += exp;
                        player.Message($"Earned {exp} experience.");
                        return;
                    }
                    if (m2 == "!boosts")
                    {
                        foreach (var c in Program.database.Characters)
                        {
                            var exp = ClassProgressionManager.ExperienceForNextLevel(c.level);
                            c.experience += exp;
                        }
                        return;
                    }
                }
                if (m2.Is("!die", "!kill", "!death", "!suicide", "!reroll") && player.character != null && player.character.currentRoomId != Guid.Empty)
                {
                    RoomManager.GetRoom(player.character.currentRoomId).DescribeAction($"**{player.GetName()}** died.");
                    player.character.OnKilled(null); // calls combat manager and removes player
                    return;
                }

                if (m2.Is("!disconnect", "!logout", "!off") && player.character != null && player.character.currentRoomId != Guid.Empty)
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
            CharacterCreator.ParsePlayerCharacterCreationInput(player, command);
        }
        public static void ParseInCharacterMessage(Character character, string command, Room room)
        {
            var characterStatus = character.characterStatus;

            var matchingCommands = InCharacterCommands.Where(x => x.IsMessageMatchingCommand(command));
            if (matchingCommands.Count() == 0)
            {
                character.Message("Error: message didn't match any known command or is missing some arguments.");
                return;
            }
            var usableCommands = matchingCommands.Where(x => x.CanUseCommand(character));
            if (usableCommands.Count() == 0)
            {
                character.Message("Impossible to use this command right now.");
                return;
            }
            if (usableCommands.Count() > 1)
            {
                character.Message($"Error: ambiguous call [{string.Join(',', usableCommands.Select(x => x.GetType().Name))}]");
                return;
            }
            usableCommands.First().Run(character, room);
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
            new CommandLock(), new CommandLockContainer(), new CommandUnlock(),new CommandUnlockContainer(),
            new CommandSay(), new CommandAct(),
            new CommandCombatAttack(), new CommandCombatFlee(),
            new CommandCastOn(), new CommandCast(),
            new CommandEquip(), new CommandUnequip(),
            new CommandSleep(), new CommandAsk(), new CommandRead(),
            new CommandTrade(), new CommandTradeList(), new CommandTradeBuy(), new CommandTradeSell(), new CommandTradeLeave(),
            new CommandSearch(), new CommandSearchLook(), new CommandSearchPickup(), new CommandSearchStash(), new CommandSearchLeave()
        };
        public static List<Command> OutCharacterCommands = new List<Command>() { };
    }
}
