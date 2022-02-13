using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SineahBot.Database.Entities;

namespace SineahBot.Database.Configurations
{
    public class PlayerEntityConfiguration : IEntityTypeConfiguration<PlayerEntity>
    {
        public void Configure(EntityTypeBuilder<PlayerEntity> builder)
        {
            builder
                .HasKey(x => x.UserId);

            System.Console.WriteLine($"Configure<{nameof(PlayerEntityConfiguration)}>");
        }
    }
}
