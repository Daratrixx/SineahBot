using SineahBot.Data;
using SineahBot.Interfaces;
using SineahBot.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SineahBot.Commands
{
    public class CommandMetaInformation : Command
    {
        public CommandMetaInformation()
        {
            commandRegex = new Regex(@"^!(i|info|self|information)$", RegexOptions.IgnoreCase);
        }

        public override void Run(Character character, Room room)
        {
            character.Message(GetCharacterInformation(character));
        }

        public string GetCharacterInformation(Character character)
        {
            var damageRedution = character.GetArmorDamageReduction();
            return $@"
**INFORMATION** - ***{character.name}***
> *{character.characterClass.ToString().ToUpper()}* *level **{character.level}*** (*{character.experience}/{ClassProgressionManager.ExperienceForNextLevel(character.level)} exp*)
> *Health* : **{character.health}/{character.MaxHealth}** (**+{character.bonusHealth}**) *regen*: **{character.GetHealthRegeneration()}** (**+{character.bonusHealthRegen}**)
> *Mana* : **{character.mana}/{character.MaxMana}** (**+{character.bonusMana}**) *regen*: **{character.GetManaRegeneration()}** (**+{character.bonusManaRegen}**)
> *Spell power* : **{character.GetSpellPower()}** ({(ClassProgressionManager.IsMagicalClass(character.characterClass) ? "2" : "1")} x level **+{character.bonusSpellPower}**)
> *Physical power* : **{character.GetWeaponDamage()}** ({(ClassProgressionManager.IsPhysicalClass(character.characterClass) ? "2" : "1")} x level **+{character.bonusDamage}**)
> *Armor*: **{character.bonusArmor}** ({(int)(damageRedution * 100)}% physical damage reduction)
> *Deflection*: **{character.bonusDeflection}**% chance to halve incoming physical damage.
> *Evasion*: **{5 + character.bonusEvasion}**% chance to halve incoming area damage.
> *Gold*: **{character.gold}**
{(character.experience >= ClassProgressionManager.ExperienceForNextLevel(character.level) ? "*You have enough experience to level up! Type **!level***" : "")}
";
        }
    }
}
