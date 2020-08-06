using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Player : IAgent
    {
        public string userId { get; set; }
        public PlayerStatus playerStatus { get; set; }
        public Guid idCharacter { get; set; }

        public Character character;

        public void Message(string message)
        {
            Console.WriteLine(message);
        }
    }


    public enum PlayerStatus
    {
        None,
        CharacterNaming,
        CharacterClassSelection,
        InCharacter,
        OutCharacter
    }
}
