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
            player = Program.database.Players.FirstOrDefault(x => x.userId == userId);
            // create and return new player
            if (player == null)
            {
                player = players[userId] = new Player()
                {
                    userId = userId,
                    playerStatus = PlayerStatus.None,
                    playerSettings = new PlayerSettings(),
                }; // todo: try loading from bdd
                Program.database.Players.Add(player);
                players[userId] = player;
                return player;
            }
            // load and returns existing player
            if (player.idCharacter != null)
            {
                player.character = CharacterManager.GetCharacter(player.idCharacter.Value);
                player.character.agent = player;
                player.playerStatus = PlayerStatus.InCharacter;
                player.playerSettings = player.settings != null ? Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerSettings>(player.settings) : new PlayerSettings();
            }
            players[userId] = player;
            return player;
        }

        public static void DisconnectPlayer(Player player)
        {
            if (player.character != null && player.character.currentRoomId != Guid.Empty.ToString())
            {
                CharacterManager.SaveCharacterInventory(player.character);
                CharacterManager.SaveCharacterEquipment(player.character);
                RoomManager.RemoveFromCurrentRoom(player.character, false);
            }
            player.settings = Newtonsoft.Json.JsonConvert.SerializeObject(player.playerSettings);
            Program.database.Update(player);
            players.Remove(player.userId);
        }

        public static void SavePlayers()
        {
            foreach (var player in players.Values)
            {
                player.settings = Newtonsoft.Json.JsonConvert.SerializeObject(player.playerSettings);
            }
            Program.database.UpdateRange(players.Values);
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
