using Discord;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SineahBot.Data
{
    public class Player : IAgent
    {
        // character creation temporary values
        public ulong channelId;
        public PlayerStatus playerStatus;

        [Key]
        public ulong userId { get; set; }
        public Guid? idCharacter { get; set; }

        public Character character;
        public CancelableMudTimer disconnectTimer = null;
        public PlayerSettings playerSettings = new PlayerSettings();

        public bool canPostError = false;

        public void SetDisconnectTimer(int minutes)
        {
            if (disconnectTimer != null)
            {
                disconnectTimer.Cancel();
                disconnectTimer = null;
            }
            if (character == null || character.currentRoomId == Guid.Empty.ToString()) return;
            disconnectTimer = new CancelableMudTimer(minutes * 60, () =>
            {
                Message("```You will be disconnected in 1 minute.```");
                disconnectTimer = new CancelableMudTimer(60, () =>
                {
                    Message("```You are now disconnected.```");
                    PlayerManager.DisconnectPlayer(this);
                    disconnectTimer = null;
                });
            });
        }

        protected List<string> messageBuffer = new List<string>();
        public void Message(string message)
        {
            if (playerSettings.Censor)
            {
                var tmp = message;
                message = CensorManager.CensorMessage(message);
                if (tmp != message)
                    Logging.Log($"Censor: \"{tmp}\"\n\t=>\t\"{message}\"");
            }
            lock (playerMessageBuffers)
            {
                lock (messageBuffer)
                {
                    messageBuffer.Add(message);
                }
                if (!playerMessageBuffers.Contains(this)) playerMessageBuffers.Add(this);
            }
        }
        public void CommitMessageBuffer()
        {
            if (messageBuffer.Count == 0) return; // nothing to commint;
            string output;
            lock (messageBuffer)
            {
                output = String.Join('\n', messageBuffer);
                messageBuffer.Clear();
            }

            if (Program.ONLINE && output != null)
            {
                var channel = Application.Discord.DiscordClient.GetChannel(channelId) as IMessageChannel;
                var result = channel.SendMessageAsync(output).Result;
                return;
            }
            Console.WriteLine(output);
        }

        private Action<Character, Room, string> messageBypassHandler = null;
        private Action<Character, Room> messageBypassCancelHandler = null;
        public void RegisterMessageBypass(Action<Character, Room, string> handler, Action<Character, Room> cancelHandler = null)
        {
            messageBypassHandler = handler;
            messageBypassCancelHandler = cancelHandler;
            Message("Type `cancel` if you want to abort the action.");
        }
        public bool HasMessageBypass()
        {
            return messageBypassHandler != null;
        }
        public void ConsumeMessageBypass(Character character, Room room, string message)
        {
            if (string.Equals(message, "cancel", StringComparison.OrdinalIgnoreCase))
            {
                Message("Action canceled.");
                messageBypassCancelHandler?.Invoke(character, room);
                messageBypassHandler = null;
                messageBypassCancelHandler = null;
                return;
            }
            messageBypassHandler?.Invoke(character, room, message);
            messageBypassHandler = null;
        }



        private static ConcurrentBag<Player> playerMessageBuffers = new ConcurrentBag<Player>();
        public static void CommitPlayerMessageBuffers()
        {
            lock (playerMessageBuffers)
            {
                foreach (var p in playerMessageBuffers)
                    p.CommitMessageBuffer();
                playerMessageBuffers.Clear();
            }
        }
        public static bool Messaging { get; private set; }
        public string GetName(IAgent agent = null)
        {
            return character?.GetName(agent);
        }
    }

    public enum PlayerStatus
    {
        None,
        CharacterCreation,
        InCharacter
    }

    public class PlayerSettings
    {
        [PlayerSettingIgnore]
        [PlayerSettingDescription("If True, display the full room description every time a room is entered")]
        public bool AutoLook { get; set; } = true;
        [PlayerSettingDescription("If True, will censor some words the bot relays to you")]
        public bool Censor { get; set; } = true;
        [PlayerSettingIgnore]
        [PlayerSettingDescription("If True, will skip Gender and Pronouns selection during character creation")]
        public bool IgnoreGender { get; set; } = true;
    }

    public class PlayerSettingIgnore : Attribute
    {
    }

    public class PlayerSettingDescription : Attribute
    {
        public string description;
        public PlayerSettingDescription(string description)
        {
            this.description = description;
        }

        public override string ToString()
        {
            return description;
        }
    }
}
