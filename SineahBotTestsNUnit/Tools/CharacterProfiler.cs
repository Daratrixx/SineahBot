using SineahBot.Data;
using System.Collections.Generic;
using System.IO;

namespace SineahBotTestsNUnit.Tools
{
    public static class CharacterProfiler
    {
        public struct ProfileData
        {
            public ProfileData(float defence, float physical, float magical, float support, float hinder)
            {
                this.defence = defence;
                this.physical = physical;
                this.magical = magical;
                this.support = support;
                this.hinder = hinder;
            }
            public float defence;
            public float physical;
            public float magical;
            public float support;
            public float hinder;
            public static ProfileData operator +(ProfileData a, ProfileData b)
            {
                return new ProfileData(a.defence + b.defence, a.physical + b.physical, a.magical + b.magical, a.support + b.support, a.hinder + b.hinder);
            }

            public override string ToString()
            {
                return $"{defence} / {physical} / {magical} / {support} / {hinder}";
            }
        }

        public static ProfileData ProfileCharacter(Character c)
        {
            ProfileData output = new ProfileData(ProfileValue(c.MaxHealth + c.GetHealthRegeneration()),
            ProfileValue(c.GetPhysicalPower()),
            ProfileValue(c.GetSpellPower()),
            ProfileValue(c.MaxMana + c.GetManaRegeneration()),
            ProfileValue(c.MaxMana + c.GetManaRegeneration()));

            foreach (Spell s in c.GetSpells())
            {
                output += ProfileSpell(s, c.GetSpellPower());
            }

            return output;
        }

        public static ProfileData ProfileSpell(Spell s, int spellPower)
        {
            ProfileData output = new ProfileData();
            foreach (var e in s.effects)
            {
                if (s.aggressiveSpell)
                {
                    if (e is Spell.Effect.AttackDamage ad)
                    {
                        output += new ProfileData(0, ProfileValue(ad.baseDamage), 0, 0, 0);
                        continue;
                    }
                    if (e is Spell.Effect.SpellDamage sd)
                    {
                        output += new ProfileData(0, 0, ProfileValue(sd.baseDamage), 0, 0);
                        continue;
                    }
                    if (e is Spell.Effect.PureDamage pd)
                    {
                        output += new ProfileData(0, ProfileValue(pd.baseDamage * 0.5f), ProfileValue(pd.baseDamage * 0.5f), 0, 0);
                        continue;
                    }
                    if (e is Spell.Effect.AddAlter aa0)
                    {
                        output += new ProfileData(0, 0, 0, 0, ProfileValue(aa0.baseDuration + spellPower * aa0.spellPowerDurationRatio));
                        continue;
                    }
                    continue;
                }
                if (e is Spell.Effect.Heal h)
                {
                    output += new ProfileData(0, 0, 0, ProfileValue(h.baseHeal), 0);
                    continue;
                }
                if (e is Spell.Effect.AddAlter aa1)
                {
                    output += new ProfileData(0, 0, 0, ProfileValue(aa1.baseDuration + spellPower * aa1.spellPowerDurationRatio), 0);
                    continue;
                }
                if (e is Spell.Effect.RemoveAlter ra)
                {
                    output += new ProfileData(0, 0, 0, .1f, .1f);
                    continue;
                }
            }
            return output;
        }

        public static float ProfileValue(float value)
        {
            return value / 100;
        }

        public static void CreateProfileFile(string fileName, IEnumerable<string> profiles)
        {
            if (!Directory.Exists("../profiles/")) Directory.CreateDirectory("../profiles/");
            if (File.Exists($"../profiles/{fileName}.txt")) File.Delete($"../profiles/{fileName}.txt");
            using (var file = File.AppendText($"../profiles/{fileName}.txt"))
            {
                foreach (var p in profiles)
                {
                    file.WriteLine(p);
                }
            }
        }
    }
}
