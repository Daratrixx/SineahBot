using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandCast : Command
    {

        public CommandCast()
        {
            commandRegex = new Regex(@"^cast (.+)$", RegexOptions.IgnoreCase);
        }

        public override void Run(IAgent agent, Room room)
        {
            if (!(agent is ICaster)) throw new Exception($@"Impossible to cast a spell as non-caster agent");
            var caster = agent as ICaster;
            if (!caster.ActionCooldownOver())
            {
                return;
            }

            var spellName = GetArgument(1);

            if (String.IsNullOrWhiteSpace(spellName))
            {
                agent.Message("What are you trying to cast ?");
                return;
            }
            var spell = caster.GetSpell(spellName);
            if (spell == null)
            {
                agent.Message($@"Can't cast unknown spell ""{spellName}"". Type **!spells** to get a list of spells you can cast.");
                return;
            }
            if (spell.NeedsTarget)
            {
                Entity target = null;
                if (spell.CanSelfCast)
                {
                    target = caster as Entity;
                }
                if (target == null && !spell.CanSelfCast)
                {
                    agent.Message($@"This spell needs a target! Type **cast {spellName} on [target name]** instead.");
                    return;
                }

                agent.Message($"You casted {spell.GetName()} on {target.name}!");
                room.DescribeAction($"{caster.GetName()} casted {spell.GetName()} on {target.name}!", agent);
                if (caster.CastSpellOn(spell, target)) // true if target died
                {
                    if (target is IKillable)
                    {
                        (target as IKillable).OnKilled(agent);
                        agent.Message($"You killed {target.GetName()}!");
                        room.DescribeAction($"{caster.GetName()} killed {target.GetName()}!", agent, target as IAgent);
                    }
                }
                caster.StartActionCooldown();
            }
            else
            {
                agent.Message($"You casted {spell.GetName()}!");
                room.DescribeAction($"{caster.GetName()} casted {spell.GetName()}!", agent);
                if (caster.CastSpell(spell)) // true if target died
                {
                    if (caster is IKillable)
                    {
                        (caster as IKillable).OnKilled();
                        agent.Message($"You killed yourself!");
                        room.DescribeAction($"{caster.GetName()} killed themselves!", agent);
                    }
                }
                caster.StartActionCooldown();
            }

            if (agent is Character) (agent as Character).experience += 1;
        }

        public override bool IsWorkbenchCommand(IAgent agent = null)
        {
            return false;
        }

    }
}
