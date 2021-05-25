using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaSettings : Command
    {
        private static IEnumerable<PropertyInfo> playerSettingProperties = typeof(PlayerSettings).GetProperties().Where(x => x.CustomAttributes.Count(x => x.AttributeType == typeof(PlayerSettingIgnore)) == 0);
        public CommandMetaSettings()
        {
            commandRegex = new Regex(@"^(!settings?)(( toggle)? (.+))?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            bool hasArgument = HasArgument(2);
            bool toggle = HasArgument(3);
            var settingName = GetArgument(4).Replace(" ", "");
            if (!hasArgument)
            {
                character.Message(GetCharacterPlayerSettings(character));
                return;
            }

            var setting = playerSettingProperties.FirstOrDefault(x => string.Equals(x.Name, settingName, StringComparison.OrdinalIgnoreCase));
            if (setting == null)
            {
                character.Message(@$"Unkown setting ""{settingName}"". Possible values: {string.Join('/', playerSettingProperties.Select(x => x.Name))}.");
                return;
            }

            var settings = (character?.agent as Player)?.playerSettings;
            if (settings == null) character.Message("Error: no settings to display.");

            if (!toggle)
            {
                character.Message($"**SETTINGS**\n{FormatSetting(settings, setting)}\nType `!settings toggle [setting name]` to change the value of a setting.");
                return;
            }

            setting.SetValue(settings, !(bool)setting.GetValue(settings));
            character.Message($"**SETTINGS**\nNew value for setting `{setting.Name}` is now *{setting.GetValue(settings)}*.");
        }
        public string GetCharacterPlayerSettings(Character character)
        {
            var settings = (character?.agent as Player)?.playerSettings;
            if (settings == null) return "Error: no settings to display.";

            var output = string.Join('\n', playerSettingProperties.Select(x => FormatSetting(settings, x)));

            return $"**SETTINGS**\n{output}\nType `!settings toggle [setting name]` to change the value of a setting.";
        }

        public static string FormatSetting(PlayerSettings settings, PropertyInfo x)
        {
            string name = x.Name;
            string value = x.GetValue(settings).ToString();
            string description = x.CustomAttributes.FirstOrDefault(y => y.AttributeType == typeof(PlayerSettingDescription))?.ConstructorArguments.FirstOrDefault().Value.ToString();
            return $"`{name}`: *{value}* ({description})";
        }
    }
}
