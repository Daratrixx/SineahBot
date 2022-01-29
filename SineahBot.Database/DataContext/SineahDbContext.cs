using Microsoft.EntityFrameworkCore;
using SineahBot.Database.Configurations;
using SineahBot.Database.Entities;
namespace SineahBot.DataContext
{
    public partial class SineahDbContext : DbContext
    {
        public SineahDbContext()
        {
            // apply migrations
            this.Database.Migrate();
        }

        public SineahDbContext(DbContextOptions<SineahDbContext> options)
            : base(options)
        {
            // apply migrations
            this.Database.Migrate();
        }

        public virtual DbSet<CharacterEntity> Characters { get; set; }

        public virtual DbSet<PlayerEntity> Players { get; set; }

        public virtual DbSet<CharacterItemEntity> CharacterItems { get; set; }

        public virtual DbSet<CharacterEquipmentEntity> CharacterEquipment { get; set; }

        public virtual DbSet<CharacterMessageEntity> CharacterMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("DataSource=../SineahBot.sqlite");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SineahDbContext).Assembly);
        }
    }
}
