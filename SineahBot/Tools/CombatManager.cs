using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SineahBot.Tools
{
    public static class CombatManager
    {
        public static Dictionary<Character, List<Character>> enemies = new Dictionary<Character, List<Character>>();

        public static void OnDamagingCharacter(Character damaging, Character damaged)
        {
            if (!enemies.ContainsKey(damaged))
            {
                enemies[damaged] = new List<Character>();
            }
            if (!enemies[damaged].Contains(damaging))
                enemies[damaged].Add(damaging);
            damaging.characterStatus = CharacterStatus.Combat;

            if (!enemies.ContainsKey(damaging))
            {
                enemies[damaging] = new List<Character>();
            }
            if (!enemies[damaging].Contains(damaged))
                enemies[damaging].Add(damaged);
            damaged.characterStatus = CharacterStatus.Combat;
        }

        public static void OnCharacterKilled(Character killed, bool direct)
        {
            var enemies = CombatManager.enemies[killed];
            var goldReward = killed.GetGoldReward() / enemies.Count;
            var expReward = killed.GetExperienceReward() / enemies.Count;
            foreach (var c in enemies)
            {
                c.Message($"Reward: {c.RewardExperience(expReward / c.level)} exp, {c.RewardGold(goldReward)} gold.");
                CombatManager.enemies[c].Remove(killed);
                if (CombatManager.enemies[c].Count == 0)
                {
                    c.Message($"You are no longer in combat.", direct);
                    c.characterStatus = CharacterStatus.Normal;
                    CombatManager.enemies.Remove(c);
                }
            }
            enemies.Remove(killed);
        }

    }
}
