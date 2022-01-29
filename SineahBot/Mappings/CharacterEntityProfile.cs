using AutoMapper;
using SineahBot.Data;
using SineahBot.Database.Entities;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SineahBot.Mappings
{
    public class CharacterEntityProfile : Profile
    {
        public CharacterEntityProfile()
        {
            this.CreateMap<string, CharacterClass>()
                .ConstructUsing((str, context) => Enum.Parse<CharacterClass>(str))
                .ReverseMap()
                .ConstructUsing((characterClass, context) => characterClass.ToString());

            this.CreateMap<ICollection<CharacterItemEntity>, Dictionary<Item, int>>()
                .ConstructUsing((entities, context) => entities.ToDictionary(k => ItemManager.GetItem(k.ItemName), v => v.StackSize))
                .ReverseMap()
                .ConstructUsing((items, context) => items.Select(kv => new CharacterItemEntity() { Id = Guid.NewGuid(), ItemName = kv.Key.name, StackSize = kv.Value }).ToList());

            this.CreateMap<CharacterEntity, Character>()
                .ForMember(x => x.id, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.characterClass, x => x.MapFrom(x => x.CharacterClass))
                .ForMember(x => x.gender, x => x.MapFrom(x => x.Gender))
                .ForMember(x => x.pronouns, x => x.MapFrom(x => x.Pronouns))
                .ForMember(x => x.level, x => x.MapFrom(x => x.Level))
                .ForMember(x => x.experience, x => x.MapFrom(x => x.Experience))
                .ForMember(x => x.gold, x => x.MapFrom(x => x.Gold))
                .ForMember(x => x.items, x => x.MapFrom(x => x.Items))
                .ForMember(x => x.equipments, x => x.Ignore())
                .ForMember(x => x.messages, x => x.MapFrom(x => x.Messages))
                .AfterMap((entity, character) =>
                {
                    foreach (var equipment in entity.Equipments)
                    {
                        var item = character.FindInInventory(equipment.ItemName) as Equipment;
                        if (item != null)
                        {
                            character.Equip(item);
                        }
                    }
                })
                .ReverseMap()
                .ForMember(x => x.Messages, x => x.Ignore())
                .ForMember(x => x.Items, x => x.Ignore())
                .ForMember(x => x.Equipments, x => x.Ignore());
        }
    }
}
