using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Player : Character
    {
        public string userId { get; set; }
        public PlayerStatus playerStatus { get; set; }
    }


    public enum PlayerStatus
    {
        None,
        CharacterNaming,
        CharacterClassSelection,
        InCharacter
    }
}
