using AutoMapper;
using SineahBot.Data;
using SineahBot.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SineahBot.Mappings
{
    public class MessageEntityProfile : Profile
    {
        public MessageEntityProfile()
        {
            this.CreateMap<CharacterMessageEntity, CharacterMessage>()
                .ForMember(x => x.id, x => x.MapFrom(x => x.Id))
                .ForMember(x => x.idCharacter, x => x.MapFrom(x => x.IdCharacter))
                .ForMember(x => x.idRoom, x => x.MapFrom(x => x.IdRoom))
                .ForMember(x => x.message, x => x.MapFrom(x => x.Message))
                .ReverseMap();

            this.CreateMap<Display.PlayerMessage, CharacterMessageEntity>()
                .ForMember(x => x.Id, x => x.MapFrom(x => x.id))
                .ForMember(x => x.IdCharacter, x => x.MapFrom(x => x.idWritter))
                .ForMember(x => x.IdRoom, x => x.MapFrom(x => x.currentRoomId))
                .ForMember(x => x.Message, x => x.MapFrom(x => x.content));
        }
    }
}
