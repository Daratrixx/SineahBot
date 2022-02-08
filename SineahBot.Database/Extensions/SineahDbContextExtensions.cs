using Microsoft.EntityFrameworkCore;
using SineahBot.Database.Entities;
using SineahBot.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SineahBot.Database.Extensions
{
    public static class SineahDbContextExtensions
    {
        private static Mutex DatabaseTranscientLock = new Mutex();

        public static PlayerEntity LoadPlayer(this SineahDbContext db, ulong userId)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                var playerEntity = db.Players
                .AsNoTracking()
                .FirstOrDefault(x => x.UserId == userId);
                return playerEntity;
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void SavePlayer(this SineahDbContext db, PlayerEntity playerEntity)
        {
            DatabaseTranscientLock.WaitOne();
            try
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

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static CharacterEntity LoadCharacter(this SineahDbContext db, Guid id)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                var characterEntity = db.Characters
                .AsNoTracking()
                .Include(x => x.Items)
                .Include(x => x.Equipments)
                .Include(x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
                return characterEntity;
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void SaveCharacter(this SineahDbContext db, CharacterEntity characterEntity)
        {
            DatabaseTranscientLock.WaitOne();
            try
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

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void RemoveCharacter(this SineahDbContext db, CharacterEntity characterEntity)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                db.CharacterItems.RemoveRange(db.CharacterItems.Where(x => x.IdCharacter == characterEntity.Id));
                db.CharacterEquipment.RemoveRange(db.CharacterEquipment.Where(x => x.IdCharacter == characterEntity.Id));
                db.Characters.Remove(characterEntity);

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static IReadOnlyCollection<CharacterMessageEntity> LoadRoomMessages(this SineahDbContext db, string idRoom)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                var roomMessagesEntities = db.CharacterMessages.AsNoTracking()
                    .Where(x => x.IdRoom == idRoom)
                    .ToArray();
                return roomMessagesEntities;
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void RemoveMessages(this SineahDbContext db, IEnumerable<CharacterMessageEntity> messages)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                db.CharacterMessages.RemoveRange(messages);

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void SaveRoomMessages(this SineahDbContext db, IEnumerable<CharacterMessageEntity> messages)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                // purge existing messages
                db.CharacterMessages.RemoveRange(messages);

                // save new messages
                db.CharacterMessages.AddRange(messages);

                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void ApplyChanges(this SineahDbContext db)
        {
            DatabaseTranscientLock.WaitOne();
            try
            {
                db.SaveChanges();
                db.ChangeTracker.Clear();
            }
            finally
            {
                DatabaseTranscientLock.ReleaseMutex();
            }
        }

        public static void Cleanup(this SineahDbContext db)
        {
            DatabaseTranscientLock.Close();
        }
    }
}
