using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SineahBot.Database.Entities
{
    public abstract class CharacterEntityBase : EntityBase
    {
        public Guid IdCharacter { get; set; }



        public CharacterEntity Character { get; set; }
    }
}
