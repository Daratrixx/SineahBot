using System;
using System.ComponentModel.DataAnnotations;

namespace SineahBot.Database.Entities
{
    public class PlayerEntity
    {
        public ulong UserId { get; set; }
        public Guid? IdCharacter { get; set; }
        public string Settings { get; set; }
    }
}
