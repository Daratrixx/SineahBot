using AutoMapper;
using SineahBot.Data;
using SineahBot.Database.Entities;
using SineahBot.Database.Extensions;
using SineahBot.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SineahBot.Tools
{
    public static class PersistenceManager
    {
        public static void ForceStaticInit() { }

        static PersistenceManager()
        {
            // init database
            Database = new SineahDbContext();

            // init mapper
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program).Assembly));

            Mapper = config.CreateMapper();
        }

        public static SineahDbContext Database { get; private set; }

        public static IMapper Mapper { get; private set; }

        // players
        public static Player LoadPlayer(ulong userId)
        {
            var playerEntity = Database.LoadPlayer(userId);
            if (playerEntity == null)
            {
                return null;
            }
            return Mapper.Map<PlayerEntity, Player>(playerEntity);
        }

        public static void SavePlayer(Player player)
        {
            var playerEntity = Mapper.Map<Player, PlayerEntity>(player);
            Database.SavePlayer(playerEntity);
        }

        public static void SavePlayers(IEnumerable<Player> players)
        {
            var playerEntities = players.Select(x => Mapper.Map<Player, PlayerEntity>(x)).ToArray();
            Database.UpdateRange(playerEntities);
        }

        // characters
        public static Character LoadCharacter(Guid id)
        {
            var characterEntity = Database.LoadCharacter(id);
            return Mapper.Map<CharacterEntity, Character>(characterEntity);
        }

        public static void SaveCharacter(Character character)
        {
            var characterEntity = Mapper.Map<Character, CharacterEntity>(character);
            Database.SaveCharacter(characterEntity);
        }

        public static void RemoveCharacter(Character character)
        {
            var messageEntities = character.messages.Select(x => Mapper.Map<CharacterMessage, CharacterMessageEntity>(x)).ToArray();
            Database.RemoveMessages(messageEntities);

            var characterEntity = Mapper.Map<Character, CharacterEntity>(character);
            Database.RemoveCharacter(characterEntity);
        }

        // room messages
        public static IEnumerable<CharacterMessage> GetRoomMessages(Room room)
        {
            var messageEntities = Database.LoadRoomMessages(room.id);
            var messages = messageEntities.Select(x => Mapper.Map<CharacterMessageEntity, CharacterMessage>(x));
            return messages;
        }

        public static void SaveRoomMessages(IEnumerable<Display.PlayerMessage> messages)
        {
            var messageEntities = messages.Select(x => Mapper.Map<Display.PlayerMessage, CharacterMessageEntity>(x)).ToArray();
            Database.SaveRoomMessages(messageEntities);
        }

        // other
        public static void SaveAll()
        {
            CharacterManager.SavePlayerCharacters();
            PlayerManager.SavePlayers();
            RoomManager.SaveRooms();
            Database.ApplyChanges();
        }
        public static void Cleanup()
        {
            Database.Cleanup();
        }
    }
}
