using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Tools
{
    public static class PlayerManager
    {
        private static Dictionary<ulong, Player> players = new Dictionary<ulong, Player>() { { 0000, TestPlayer } };

        public static Player GetPlayer(ulong userId)
        {
            if (!players.ContainsKey(userId))
                players[userId] = new Player()
                {
                    userId = userId,
                    playerStatus = PlayerStatus.None,
                }; // todo: try loading from bdd
            return players[userId];
        }

        public static Player TestPlayer
        {
            get
            {
                var character = CharacterManager.TestCharacter;
                var player = new Player()
                {
                    playerStatus = PlayerStatus.InCharacter,
                    //characterStatus = CharacterStatus.Normal,
                    //id = Guid.NewGuid(),
                    //name = "test player",
                    //description = "You notice the test player.",
                    userId = 0000,
                    character = character
                };
                character.agent = player;
                
                return player;
            }
        }
    }
}
