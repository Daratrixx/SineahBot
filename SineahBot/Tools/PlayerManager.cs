using SineahBot.Data;
using SineahBot.Database.Entities;
using SineahBot.Database.Extensions;
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
            player = LoadPlayer(userId);
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
                return player;
            }
            // load and returns existing player
            if (player.idCharacter != null)
            {
                player.character = CharacterManager.GetCharacter(player.idCharacter.Value);
                player.character.agent = player;
                player.playerStatus = PlayerStatus.InCharacter;
            }
            players[userId] = player;
            return player;
        }

        public static Player LoadPlayer(ulong userId)
        {
            var playerEntity = Program.Database.LoadPlayer(userId);
            if (playerEntity == null)
            {
                return null;
            }
            return Program.Mapper.Map<PlayerEntity, Player>(playerEntity);
        }

        public static void SavePlayer(Player player)
        {
            var playerEntity = Program.Mapper.Map<Player, PlayerEntity>(player);
            Program.Database.SavePlayer(playerEntity);
        }
        public static void SavePlayers()
        {
            var playerEntities = players.Values.Select(x => Program.Mapper.Map<Player, PlayerEntity>(x)).ToArray();
            Program.Database.UpdateRange(playerEntities);
        }

        public static void DisconnectPlayer(Player player)
        {
            if (player.character != null && player.character.currentRoomId != Guid.Empty.ToString())
            {
                CharacterManager.SaveCharacter(player.character);
                RoomManager.RemoveFromCurrentRoom(player.character, false);
            }
            SavePlayer(player);
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
