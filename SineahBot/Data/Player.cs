using Discord;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        public void SetDisconnectTimer(int minutes)
        {
            if (disconnectTimer != null)
            {
                disconnectTimer.Cancel();
                disconnectTimer = null;
            }
            if (character == null || character.currentRoomId == Guid.Empty) return;
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
                var channel = Program.DiscordClient.GetChannel(channelId) as IMessageChannel;
                var result = channel.SendMessageAsync(output).Result;
                return;
            }
            Console.WriteLine(output);
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
}
