using System;
using System.ComponentModel.DataAnnotations;

namespace SineahBot.Database.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
