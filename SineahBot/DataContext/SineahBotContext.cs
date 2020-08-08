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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
