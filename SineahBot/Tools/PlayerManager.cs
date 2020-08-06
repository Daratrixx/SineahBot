using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Tools
{
    public static class PlayerManager
    {
        private static Dictionary<string, Player> players = new Dictionary<string, Player>() { { "test", TestPlayer } };

        public static Player GetPlayer(string userId)
        {
            if (!players.ContainsKey(userId))
                players[userId] = new Player()
                {
                    userId = userId,
                    playerStatus = PlayerStatus.None,
                    //characterStatus = CharacterStatus.Normal,
                    //id = Guid.NewGuid()
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
                    userId = "test",
                    character = character
                };
                character.agent = player;
                
                return player;
            }
        }
    }
}
