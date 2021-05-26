using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Tools
{
    public static class CensorManager
    {

        private static readonly Regex urlRegex = new Regex(@"(https?:\/\/)?(www\.)?(([a-zA-Z1-9-_\.]+)\.([a-zA-Z]+))(\/[^ >]*)?");
        private static readonly string[] urlWhitelist = new string[] {
            "youtube.com",
            "discord.com",
        };
        // must be run on every player-generated content
        public static string FilterMessage(string message)
        {
            message = FilterMessageUrl(message);
            return message;
        }

        public static readonly string urlFilterReplace = "[filtered url]";
        public static string FilterMessageUrl(string message, int offset = 0)
        {
            var urlMatch = urlRegex.Match(message, offset);
            if (!urlMatch.Success) return message; // no url detected in the remaining message, return

            if (!urlWhitelist.Contains(urlMatch.Groups[3].Value)) // not a whitelisted domain
            {
                message = message.Substring(0, urlMatch.Index) + urlFilterReplace + message.Substring(urlMatch.Index + urlMatch.Length); // remove link
                return FilterMessageUrl(message, urlMatch.Index + urlFilterReplace.Length); // check again beyond what was filtered
            }

            return FilterMessageUrl(message, urlMatch.Index + urlMatch.Length); // check again beyond what was not filtered
        }

        private static string[] bannedWords = new string[] {
            "nigger",
            "nigga",
            "dick",
            "cock",
            "penis",
            "ass",
            "boobs",
            "pussy",
            "cunt",
            "vagina",
            "clitoris",
            "clit",
            "slut",
            "whore",
            "hoe",
        };

        // must be run when forwarding content to player with censor toggled on
        public static string CensorMessage(string message)
        {
            // filter words
            foreach (var w in bannedWords)
            {
                if (message.Contains(w, StringComparison.OrdinalIgnoreCase))
                    message = message.Replace(w, Stars(w.Length));
            }
            return message;
        }
        public static string Stars(int length)
        {
            char[] output = new char[length];
            for (int i = 0; i < length; ++i)
                output[i] = '*';
            return new string(output);
        }
    }
}
