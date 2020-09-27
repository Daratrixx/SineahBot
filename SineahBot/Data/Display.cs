using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Display : Entity, IObservable
    {
        public Display(string displayName, string[] alternativeNames = null) : base()
        {
            name = displayName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
        }
        public string[] alternativeNames = new string[] { };
        public string description { get; set; }
        public string content { get; set; }

        public virtual string GetFullDescription(IAgent agent = null)
        {
            return $"{content}";
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }

        public static Display Sign(string signName, string signContent)
        {
            return new Display(signName, new string[] { signName.Replace(" ", ""), "sign" }) { content = "The sign reads:\n> " + signContent.Replace("\n","\n> "), description = $"You spot the {signName}." };
        }
    }
}
