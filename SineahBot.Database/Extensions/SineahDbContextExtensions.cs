using Microsoft.EntityFrameworkCore;
using SineahBot.Database.Entities;
using SineahBot.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBot.Database.Extensions
{
    public static class SineahDbContextExtensions
    {
        public static PlayerEntity LoadPlayer(this SineahDbContext db, ulong userId)
        {
            var playerEntity = db.Players
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == userId);
            return playerEntity;
        }

        public static void SavePlayer(this SineahDbContext db, PlayerEntity playerEntity)
        {
            var exists = db.Players.AsQueryable()
                .Where(x => x.UserId == playerEntity.UserId)
                .Count() > 0;
            if (exists)
            {
                db.Players.Update(playerEntity);
            }
            else
            {
                db.Players.Add(playerEntity);
            }

            db.ApplyChanges();
        }

        public static CharacterEntity LoadCharacter(this SineahDbContext db, Guid id)
        {
            var characterEntity = db.Characters
                .AsNoTracking()
                .Include(x => x.Items)
                .Include(x => x.Equipments)
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return characterEntity;
        }

        public static void SaveCharacter(this SineahDbContext db, CharacterEntity characterEntity)
        {
            var exists = db.Characters.AsQueryable()
                .Where(x => x.Id == characterEntity.Id)
                .Count() > 0;
            if (exists)
            {
                db.Characters.Update(characterEntity);
            }
            else
            {
                db.Characters.Add(characterEntity);
            }

            db.ApplyChanges();
        }

        public static void RemoveCharacter(this SineahDbContext db, CharacterEntity characterEntity)
        {
            db.CharacterItems.RemoveRange(db.CharacterItems.Where(x => x.IdCharacter == characterEntity.Id));
            db.CharacterEquipment.RemoveRange(db.CharacterEquipment.Where(x => x.IdCharacter == characterEntity.Id));
            db.Characters.Remove(characterEntity);

            db.ApplyChanges();
        }

        public static IReadOnlyCollection<CharacterMessageEntity> LoadRoomMessages(this SineahDbContext db, string idRoom)
        {
            var roomMessagesEntities = db.CharacterMessages.AsNoTracking()
                .Where(x => x.IdRoom == idRoom)
                .ToArray();
            return roomMessagesEntities;
        }

        public static void RemoveMessages(this SineahDbContext db, IEnumerable<CharacterMessageEntity> messages)
        {
            db.CharacterMessages.RemoveRange(messages);

            db.ApplyChanges();
        }

        public static void SaveRoomMessages(this SineahDbContext db, IEnumerable<CharacterMessageEntity> messages)
        {
            // purge existing messages
            db.RemoveMessages(db.CharacterMessages);

            // save new messages
            db.CharacterMessages.AddRange(messages);

            db.ApplyChanges();
        }

        public static void ApplyChanges(this SineahDbContext db)
        {
            db.SaveChanges();
            db.ChangeTracker.Clear();
        }
    }
}
