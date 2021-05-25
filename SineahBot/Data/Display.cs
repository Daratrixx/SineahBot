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
        public string description { get; set; }
        public string details { get; set; }
        public string[] content { get; set; } = new string[] { };

        public virtual string GetFullDescription(IAgent agent = null)
        {
            return details;
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return description;
        }

        public string GetContent(Character reader = null, int page = 0)
        {
            if (content.Length == 0) return details;
            return content[Math.Clamp(page, 0, content.Length - 1)];
        }
        public bool HasMultiplePages()
        {
            return content.Length > 1;
        }

        public int GetPageCount()
        {
            return content.Length;
        }

        public static Display Sign(string signName, string details, string signContent = null)
        {
            return new Display(signName, new string[] { signName.Replace(" ", ""), "sign" })
            {
                description = $"You spot a sign labelled \"**{signName}**\".",
                details = "The sign reads:\n> " + details.Replace("\n", "\n> "),
                content = new string[] { "The sign reads:\n> " + (!string.IsNullOrWhiteSpace(signContent) ? signContent : details).Replace("\n", "\n> ") },
            };
        }

        public static Display Book(string bookName, string details, params string[] content)
        {
            return new Display(bookName, new string[] { bookName.Replace(" ", ""), "book" })
            {
                description = $"You found a book labelled \"**{bookName}**\".",
                details = details,
                content = content,
            };
        }

        public class PlayerMessage : Display
        {
            public readonly Character writter;
            public PlayerMessage(Character character, string message) : base("Message", new string[] { })
            {
                description = "You spot a message.";
                details = $"Someone left this message here.\n> Type `read message` to read it.";
                content = new string[] { message };
                writter = character;
            }
        }
    }
}
