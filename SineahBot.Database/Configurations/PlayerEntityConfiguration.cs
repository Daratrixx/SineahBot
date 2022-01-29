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

            builder
                .HasOne(x => x.Character)
                .WithOne(x => x.Player)
                .HasForeignKey<PlayerEntity>(x => x.IdCharacter)
                .HasPrincipalKey<CharacterEntity>(x => x.Id);

            System.Console.WriteLine($"Configure<{nameof(PlayerEntityConfiguration)}>");
        }
    }
}
