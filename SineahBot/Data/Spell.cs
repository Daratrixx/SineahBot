using SineahBot.Data.Spells;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Spell : INamed
    {
        public Guid id;
        public string name;
        public int manaCost = 0;
        public bool needsTarget = true;
        public bool canSelfCast = true;
        public Spell(string spellName, string[] alternativeNames = null)
        {
            id = Guid.NewGuid();
            name = spellName;
            if (alternativeNames != null) this.alternativeNames = alternativeNames.Select(x => x.ToLower()).ToArray();
        }
        public string[] alternativeNames = new string[] { };
        public string description { get; set; }

        public Effect[] effects = new Effect[] { };
        public virtual string GetDescription(ICaster caster = null)
        {
            return description;
        }
        public virtual string GetEffectDescription(ICaster caster = null)
        {
            return string.Join("\n", effects.Select(x => x.GetEffectDescription(caster)));
        }

        public void Cast(ICaster caster, Entity target)
        {
            foreach (var effect in effects)
            {
                effect.RunEffect(caster, target);
            }
        }

        public string GetName(IAgent agent = null)
        {
            return name;
        }
        public abstract class Effect
        {
            public abstract void RunEffect(ICaster caster, Entity target);
            public abstract string GetEffectDescription(ICaster caster = null);

            public class Heal : Effect
            {
                public int baseHeal = 0;

                public override string GetEffectDescription(ICaster caster = null)
                {
                    if (caster != null)
                    {
                        return $"- Heals target\n> Healing potential : **{baseHeal + caster.GetSpellPower()} ({baseHeal} + [spell power])**";
                    }
                    else
                    {
                        return $"- Heals target\n> Healing potential : **{baseHeal} +  [spell power]**";
                    }
                }

                public override void RunEffect(ICaster caster, Entity target)
                {
                    if (target is IHealable)
                    {
                        var healingAmount = baseHeal + caster.GetSpellPower() + new Random().Next(5, 10);
                        (target as IHealable).RestoreHealth(healingAmount, caster);
                    }
                }
            }
            public class SpellDamage : Effect
            {
                public int baseDamage = 0;

                public override string GetEffectDescription(ICaster caster = null)
                {
                    if (caster != null)
                    {
                        return $"- Damages target\n> Damaging potential : **{baseDamage + caster.GetSpellPower()} ({baseDamage} + [spell power])**";
                    }
                    else
                    {
                        return $"- Damages target\n> Damaging potential : **{baseDamage} + [spell power]**";
                    }
                }

                public override void RunEffect(ICaster caster, Entity target)
                {
                    if (target is IDamageable)
                    {
                        var damageAmount = baseDamage + caster.GetSpellPower() + new Random().Next(5, 10);
                        (target as IDamageable).DamageHealth(damageAmount, caster as Entity);
                    }
                }
            }
            public class AttackDamage : Effect
            {
                public int baseDamage = 0;

                public override string GetEffectDescription(ICaster caster = null)
                {
                    if (caster != null)
                    {
                        return $"- Damages target\n> Damaging potential : **{baseDamage + caster.GetPhysicalPower()} ({baseDamage} + [physical power])**";
                    }
                    else
                    {
                        return $"- Damages target\n> Damaging potential : **{baseDamage} + [physical power]**";
                    }
                }

                public override void RunEffect(ICaster caster, Entity target)
                {
                    if (target is IDamageable)
                    {
                        var damageAmount = baseDamage + caster.GetPhysicalPower() + new Random().Next(5, 10);
                        (target as IDamageable).DamageHealth(damageAmount, caster as Entity);
                    }
                }
            }
            public class AddAlter : Effect
            {
                public AlterationType alteration;
                public int baseDuration;
                public double spellPowerDurationRatio = 1;

                public override string GetEffectDescription(ICaster caster = null)
                {
                    if (caster != null)
                    {
                        return $"- Applies alteration ({alteration})" +
                        $"\n> Duration : **{baseDuration + (int)(caster.GetSpellPower() * spellPowerDurationRatio)} ({baseDuration} + [spell power] * {spellPowerDurationRatio})**";
                    }
                    else
                    {
                        return $"- Applies alteration ({alteration})" +
                        $"\n> Duration : **{baseDuration} + [spell power] * {spellPowerDurationRatio}**";
                    }
                }

                public override void RunEffect(ICaster caster, Entity target)
                {
                    if (target is Character)
                    {
                        var c = target as Character;
                        var duration = baseDuration + (int)(caster.GetSpellPower() * spellPowerDurationRatio);
                        c.AddAlteration(alteration, duration, caster is NPC);
                    }
                }
            }
            public class RemoveAlter : Effect
            {
                public AlterationType alteration;

                public override string GetEffectDescription(ICaster caster = null)
                {
                    return $"- Removes alteration ({alteration})";
                }

                public override void RunEffect(ICaster caster, Entity target)
                {
                    if (target is Character)
                    {
                        var c = target as Character;
                        c.RemoveAlteration(alteration, caster is NPC);
                    }
                }
            }
            public class Custom : Effect
            {
                public Custom(EffectRunHandler runHandler, EffectDescriptionHandler descriptionHandler)
                {
                    this.runHandler = runHandler;
                    this.descriptionHandler = descriptionHandler;
                }
                private EffectRunHandler runHandler;
                private EffectDescriptionHandler descriptionHandler;

                public override void RunEffect(ICaster caster, Entity target)
                {
                    runHandler(caster, target);
                }

                public override string GetEffectDescription(ICaster caster = null)
                {
                    return descriptionHandler(caster);
                }
            }

            public delegate void EffectRunHandler(ICaster caster, Entity target);
            public delegate string EffectDescriptionHandler(ICaster caster = null);
        }
    }
}
