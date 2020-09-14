using Discord;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string characterName;
        public CharacterClass characterClass;
        public PlayerStatus playerStatus;
        public PlayerCharacterCreationStatus playerCharacterCreationStatus;

        [Key]
        public ulong userId { get; set; }
        public Guid? idCharacter { get; set; }

        public Character character;


        protected List<string> messageMuffer = new List<string>();
        public void Message(string message, bool direct = false)
        {
            if (direct)
            {
                if (Program.ONLINE && !string.IsNullOrWhiteSpace(message))
                {
                    var channel = Program.DiscordClient.GetChannel(channelId) as IMessageChannel;
                    var result = channel.SendMessageAsync(message).Result;
                }
                else
                    Console.WriteLine(message);
            }
            else
            {
                Task.Run(() =>
                {
                    //if (!Monitor.IsEntered(playerMessageBuffers))
                    //    Monitor.TryEnter(playerMessageBuffers, 5000);
                    messageMuffer.Add(message);
                    if (!playerMessageBuffers.Contains(this)) playerMessageBuffers.Add(this);
                    //Monitor.Exit(playerMessageBuffers);
                }).Wait();
            }
        }
        public void CommitMessageBuffer()
        {
            var output = String.Join('\n', messageMuffer);
            if (Program.ONLINE && output != null)
            {
                var channel = Program.DiscordClient.GetChannel(channelId) as IMessageChannel;
                var result = channel.SendMessageAsync(output).Result;
            }
            else
                Console.WriteLine(output);
            messageMuffer.Clear();
        }

        private static List<Player> playerMessageBuffers = new List<Player>();
        public static async Task CommitPlayerMessageBuffers()
        {
            await Task.Run(() =>
            {
                if (Messaging) return;
                //if (!Monitor.IsEntered(playerMessageBuffers))
                //    Monitor.TryEnter(playerMessageBuffers, 5000);
                Messaging = true;
                foreach (var p in playerMessageBuffers)
                    p.CommitMessageBuffer();
                playerMessageBuffers.Clear();
                Messaging = false;
                //Monitor.Exit(playerMessageBuffers);
            });
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
        InCharacter,
        OutCharacter
    }

    public enum PlayerCharacterCreationStatus
    {
        None,
        Naming,
        NamingConfirmation,
        Classing,
        ClassingConfirmation,
    }
}
