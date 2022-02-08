using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SineahBot.Database.Entities;

namespace SineahBot.Database.Configurations
{
    public class CharacterEntityConfiguration : EntityBaseConfiguration<CharacterEntity>
    {
        public override void Configure(EntityTypeBuilder<CharacterEntity> builder)
        {
            base.Configure(builder);

            builder
                .Property(e => e.Name)
                .IsRequired();

            builder
                .Property(e => e.Gender)
                .IsRequired();

            builder
                .Property(e => e.Pronouns)
                .IsRequired();

            builder
                .Property(e => e.CharacterAncestry)
                .HasDefaultValue("Human");

            builder
                .HasOne(x => x.Player)
                .WithOne(x => x.Character)
                .HasForeignKey<PlayerEntity>(x => x.IdCharacter)
                .HasPrincipalKey<CharacterEntity>(x => x.Id);

            System.Console.WriteLine($"Configure<{nameof(CharacterEntityConfiguration)}>");
        }
    }
}
