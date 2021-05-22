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

        public static void EngageCombat(Character character, params Character[] targets)
        {
            foreach (var target in targets)
            {
                OnDamagingCharacter(character, target);
            }
        }
        public static void OnDamagingCharacter(Character damaging, Character damaged)
        {
            lock (enemies)
            {
                if (damaging == damaged) return; // don't start a combat against yourself...
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
        }
        public static void OnCharacterKilled(Character killed)
        {
            lock (enemies)
            {
                if (enemies.ContainsKey(killed))
                {
                    var enemies = CombatManager.enemies[killed].Where(x => !x.HasCharacterTag(CharacterTag.Summon));
                    if (enemies.Count() > 0)
                    {
                        var goldReward = killed.GetGoldReward() / enemies.Count();
                        var expReward = killed.GetExperienceReward() / enemies.Count();
                        foreach (var c in enemies)
                        {
                            c.Message($"Reward: {c.RewardExperience(expReward / c.level)} exp, {c.RewardGold(goldReward)} gold.");
                        }
                    }
                }
            }
            RemoveFromCombat(killed);
        }
        public static IEnumerable<Character> GetOpponents(Character character)
        {
            lock (enemies)
            {
                if (enemies.TryGetValue(character, out var enemyList))
                {
                    return enemyList.ToArray();
                }
            }
            return null;
        }

        public static void RemoveFromCombat(Character character)
        {
            lock(enemies)
            {
                if (enemies.TryGetValue(character, out var characterEnemies))
                {
                    foreach (var enemy in characterEnemies)
                    {
                        enemies[enemy].Remove(character);
                        if (enemies[enemy].Count == 0)
                        {
                            enemies.Remove(enemy);
                            enemy.characterStatus = CharacterStatus.Normal;
                            enemy.Message($"You are no longer in combat.");
                        }
                    }
                    enemies.Remove(character);
                    character.characterStatus = CharacterStatus.Normal;
                    character.Message($"You are no longer in combat.");
                }
            }
        }
    }
}
