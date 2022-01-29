using AutoMapper;
using Newtonsoft.Json;
using SineahBot.Data;
using SineahBot.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SineahBot.Mappings
{
    public class PlayerEntityProfile : Profile
    {
        public PlayerEntityProfile()
        {
            this.CreateMap<PlayerEntity, Player>()
                .ForMember(x => x.userId, x => x.MapFrom(x => x.UserId))
                .ForMember(x => x.character, x => x.Ignore())
                .ForMember(x => x.idCharacter, x => x.MapFrom(x => x.IdCharacter))
                .ForMember(x => x.playerSettings, x => x.MapFrom(x => JsonConvert.DeserializeObject<PlayerSettings>(x.Settings)))
                .ReverseMap()
                .ForMember(x => x.Settings, x => x.MapFrom(x => JsonConvert.SerializeObject(x.playerSettings)))
                .ForMember(x => x.Character, x => x.Ignore());
        }
    }
}
