using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaSpells : Command
    {

        public CommandMetaSpells()
        {
            commandRegex = new Regex(@"^(!spells)( .+)?$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            var spellName = GetArgument(2);
            DisplayinformationForCharacter(character, spellName);
        }
        public void DisplayinformationForCharacter(Character character, string spellName)
        {
            if (character.spells.Length == 0)
            {
                character.Message($@"> You do not know any spells yet.");
                return;
            }
            if (!String.IsNullOrEmpty(spellName))
            {
                var spell = character.GetSpell(spellName);
                if (spell == null)
                {
                    character.Message($@"Impossible to display information for unknown spell ""{spellName}""");
                }
                else
                {
                    character.Message(GetSpellDetails(spell, character as ICaster));
                }
            }
            else
            {
                character.Message(GetCharacterSpells(character));
            }
        }

        public string GetCharacterSpells(Character character)
        {
            return "SPELLS\n"
            + String.Join('\n', character.spells.Select(x => GetSpellInformation(x, character as ICaster)))
            + "\n *Type **!spells [spell name]** to get more informations about the specified spell.*";
        }
        public string GetSpellInformation(Spell spell, ICaster caster)
        {
            return $"> {spell.GetName()} - {spell.GetDescription(caster)}";
        }
        public string GetSpellDetails(Spell spell, ICaster caster)
        {
            return $@"
**{spell.GetName().ToUpper()}** *(alt: {String.Join(", ", spell.alternativeNames)})*
> {spell.GetDescription(caster)}
> {(spell.NeedsTarget ? "Needs target" : "No target")}, {(spell.CanSelfCast ? "Can self cast" : "Can't self cast")}
> Mana cost : {spell.manaCost}
> Effects : {spell.GetEffectDescription(caster)}
";
        }

    }
}
