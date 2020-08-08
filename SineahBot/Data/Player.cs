using Discord;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Player : IAgent
    {
        public ulong userId { get; set; }
        public ulong channelId;
        public string characterName;
        public PlayerStatus playerStatus { get; set; }
        public PlayerCharacterCreationStatus playerCharacterCreationStatus { get; set; }
        public Guid idCharacter { get; set; }

        public Character character;

        public void Message(string message)
        {
            if (Program.ONLINE)
            {
                var channel = Program.DiscordClient.GetChannel(channelId) as IMessageChannel;
                var result = channel.SendMessageAsync(message).Result;
            }
            else
                Console.WriteLine(message);
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
        NamingConfirmation
    }
}
