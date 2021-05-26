using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SineahBot.DataContext
{
    public partial class SineahBotContext : DbContext
    {
        public SineahBotContext()
        {
        }

        public SineahBotContext(DbContextOptions<SineahBotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Data.Character> Characters { get; set; }
        public virtual DbSet<Data.Player> Players { get; set; }
        public virtual DbSet<Data.CharacterItem> CharacterItems { get; set; }
        public virtual DbSet<Data.CharacterEquipment> CharacterEquipment { get; set; }
        public virtual DbSet<Data.CharacterMessage> CharacterMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=../SineahBot.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data.Character>(entity =>
            {
                entity.HasIndex(e => e.id)
                    .IsUnique();

                entity.Property(e => e.name)
                    .IsRequired();

                entity.Property(e => e.gender)
                    .IsRequired();

                entity.Property(e => e.pronouns)
                    .IsRequired();
            });

            modelBuilder.Entity<Data.Player>(entity =>
            {
                entity.HasIndex(e => e.userId)
                    .IsUnique();

                entity.Property(e => e.userId)
                    .ValueGeneratedNever();

                entity.HasIndex(e => e.idCharacter)
                    .IsUnique();
            });

            modelBuilder.Entity<Data.CharacterItem>(entity =>
            {
                entity.HasIndex(e => e.id);
            });

            modelBuilder.Entity<Data.CharacterEquipment>(entity =>
            {
                entity.HasIndex(e => e.id);
            });

            modelBuilder.Entity<Data.CharacterMessage>(entity =>
            {
                entity.HasIndex(e => e.id);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
