using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SineahBot.Data
{
    public class DataItem
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public string[] alternativeNames = new string[] { };
    }
}
