using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Tools
{
    public static class PlayerManager
    {
        private static Dictionary<ulong, Player> players = new Dictionary<ulong, Player>() { { 0000, TestPlayer } };

        public static Player GetPlayer(ulong userId)
        {
            if (!players.ContainsKey(userId))
            {
                Player player = Program.database.Players.FirstOrDefault(x => x.userId == userId);
                if (player == null)
                {
                    player = players[userId] = new Player()
                    {
                        userId = userId,
                        playerStatus = PlayerStatus.None,
                    }; // todo: try loading from bdd
                    Program.database.Players.Add(player);
                }
                else
                {
                    if (player.idCharacter != null)
                    {
                        player.character = CharacterManager.GetCharacter(player.idCharacter.Value);
                        player.character.agent = player;
                        player.playerStatus = PlayerStatus.InCharacter;
                    }
                }
                return player;
            }
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
