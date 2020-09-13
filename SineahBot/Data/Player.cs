using Discord;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

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
        public void Message(string message)
        {
            messageMuffer.Add(message);
            if (!playerMessageBuffers.Contains(this)) playerMessageBuffers.Add(this);
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
        public static void CommitPlayerMessageBuffers()
        {
            foreach (var p in playerMessageBuffers)
                p.CommitMessageBuffer();
            playerMessageBuffers.Clear();
        }
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
