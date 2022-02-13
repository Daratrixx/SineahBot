using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Tools
{
    public static class PlayerManager
    {
        private static Dictionary<ulong, Player> players = new Dictionary<ulong, Player>() { };

        public static Player GetPlayer(ulong userId)
        {
            // player already loaded
            if (players.TryGetValue(userId, out Player player))
                return player;

            // attempt to load player from database
            player = PersistenceManager.LoadPlayer(userId);

            // create and return new player
            if (player == null)
            {
                player = players[userId] = new Player()
                {
                    userId = userId,
                    playerStatus = PlayerStatus.None,
                    playerSettings = new PlayerSettings(),
                };
                players[userId] = player;
                PersistenceManager.SavePlayer(player);
                return player;
            }

            // load and returns existing player
            if (player.idCharacter != null)
            {
                try
                {
                    player.character = CharacterManager.GetCharacter(player.idCharacter.Value);
                    player.character.agent = player;
                    player.playerStatus = PlayerStatus.InCharacter;
                }
                catch
                {
                    player.idCharacter = null;
                    player.playerStatus = PlayerStatus.CharacterCreation;
                }
            }
            players[userId] = player;
            return player;
        }

        public static void SavePlayers()
        {
            PersistenceManager.SavePlayers(players.Values);
        }

        public static void DisconnectPlayer(Player player)
        {
            if (player.character != null && player.character.currentRoomId != Guid.Empty.ToString())
            {
                PersistenceManager.SaveCharacter(player.character);
                RoomManager.RemoveFromCurrentRoom(player.character, false);
            }
            PersistenceManager.SavePlayer(player);
            players.Remove(player.userId);
        }

        public static Player CreateTestPlayer()
        {
            var character = CharacterManager.CreateTestCharacter();
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
            players[0] = player;

            return player;
        }
    }
}
