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

        public void Message(string message)
        {
            if (Program.ONLINE && message != null)
            {
                var channel = Program.DiscordClient.GetChannel(channelId) as IMessageChannel;
                var result = channel.SendMessageAsync(message).Result;
            }
            else
                Console.WriteLine(message);
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
