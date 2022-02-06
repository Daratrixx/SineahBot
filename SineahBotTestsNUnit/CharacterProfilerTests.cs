using NUnit.Framework;
using SineahBot.Data;
using SineahBot.Data.Enums;
using SineahBot.Tools;
using SineahBotTestsNUnit.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SineahBotTestsNUnit
{
    public class CharacterProfilerTests
    {
        private IEnumerable<CharacterClass> classList = ClassProgressionManager.magicalClass.Union(ClassProgressionManager.physicalClass);

        [Test]
        public void TestLevel1Classes()
        {
            ProfileAllClassForLevel(1);
            ProfileAllClassForLevel(3);
            ProfileAllClassForLevel(5);
            ProfileAllClassForLevel(10);
            ProfileAllClassForLevel(20);
        }

        private void ProfileAllClassForLevel(int level)
        {
            List<string> profiles = new List<string>();
            foreach (var c in classList)
            {
                Character character = new Character() { characterClass = c, level = level };
                ClassProgressionManager.ApplyClassProgressionForCharacter(character);
                var profile = CharacterProfiler.ProfileCharacter(character);
                var profileStr = $"{c} ({level})\n{profile}";
                Console.WriteLine(profileStr);
                profiles.Add(profileStr);
            }
            CharacterProfiler.CreateProfileFile($"level{level}", profiles);
        }

    }
}