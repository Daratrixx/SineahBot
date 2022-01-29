using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SineahBot.Database.Entities;

namespace SineahBot.Database.Configurations
{
    public class CharacterEquipmentEntityConfiguration : EntityBaseConfiguration<CharacterEquipmentEntity>
    {
        public override void Configure(EntityTypeBuilder<CharacterEquipmentEntity> builder)
        {
            base.Configure(builder);

            builder
                .HasOne(x => x.Character)
                .WithMany(x => x.Equipments)
                .HasForeignKey(x => x.IdCharacter)
                .HasPrincipalKey(x => x.Id);

            System.Console.WriteLine($"Configure<{nameof(CharacterEquipmentEntityConfiguration)}>");
        }
    }
}
