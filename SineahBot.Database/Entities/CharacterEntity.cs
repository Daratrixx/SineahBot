using System.Collections.Generic;

namespace SineahBot.Database.Entities
{
    public class CharacterEntity : EntityBase
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public string Pronouns { get; set; }

        public string CharacterClass { get; set; }

        public int Level { get; set; }

        public int Experience { get; set; }

        public int Gold { get; set; }

        public PlayerEntity Player { get; set; }

        public ICollection<CharacterItemEntity> Items { get; set; }

        public ICollection<CharacterEquipmentEntity> Equipments { get; set; }

        public ICollection<CharacterMessageEntity> Messages { get; set; }

        public override string ToString()
        {
            return $"Character ({Name}-{Id})";
        }
    }
}
