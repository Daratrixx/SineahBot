using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SineahBot.Database.Entities;

namespace SineahBot.Database.Configurations
{
    public class CharacterMessageEntityConfiguration : EntityBaseConfiguration<CharacterMessageEntity>
    {
        public override void Configure(EntityTypeBuilder<CharacterMessageEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Character)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.IdCharacter)
                .HasPrincipalKey(x => x.Id);

            System.Console.WriteLine($"Configure<{nameof(CharacterMessageEntityConfiguration)}>");
        }
    }
}
