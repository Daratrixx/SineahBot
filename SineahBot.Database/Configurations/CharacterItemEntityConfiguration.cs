using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SineahBot.Database.Entities;

namespace SineahBot.Database.Configurations
{
    public class CharacterItemEntityConfiguration : EntityBaseConfiguration<CharacterItemEntity>
    {
        public override void Configure(EntityTypeBuilder<CharacterItemEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Character)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.IdCharacter)
                .HasPrincipalKey(x => x.Id);

            System.Console.WriteLine($"Configure<{nameof(CharacterItemEntityConfiguration)}>");
        }
    }
}
