using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Tools
{
    public static class CensorManager
    {
        private static readonly string censorFilePath = "../config/censor.txt";
        private static readonly string whitelistFilePath = "../config/whitelist.txt";
        static CensorManager()
        {
            var censorLines = File.ReadAllLines(censorFilePath);
            var whitelistLines = File.ReadAllLines(whitelistFilePath);
            bannedWords = censorLines;
            urlWhitelist = whitelistLines;
        }
        private static readonly string[] urlWhitelist;
        private static string[] bannedWords;

        private static readonly Regex urlRegex = new Regex(@"(https?:\/\/)?(www\.)?(([a-zA-Z1-9-_\.]+)\.([a-zA-Z]+))(\/[^ >]*)?");
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

        // must be run when forwarding content to player with censor toggled on
        public static string CensorMessage(string message)
        {
            var builder = new StringBuilder(message);
            // filter words
            foreach (var w in bannedWords)
            {
                var matches = new Regex(@$"(^|\W)({w})(\W|$)", RegexOptions.IgnoreCase | RegexOptions.Multiline).Matches(message).AsEnumerable();
                foreach (var match in matches)
                {
                    builder
                        .Remove(match.Groups[2].Index, match.Groups[2].Length)
                        .Insert(match.Groups[2].Index, Stars(match.Groups[2].Length));
                }
            }
            return builder.ToString();
        }
        public static string Stars(int length)
        {
            char[] output = new char[length * 2];
            for (int i = 0; i < length; ++i)
            {
                output[i * 2] = '\\';
                output[i * 2 + 1] = '*';
            }
            return new string(output);
        }
    }
}
