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

            System.Console.WriteLine($"Configure<{nameof(CharacterEntityConfiguration)}>");
        }
    }
}
