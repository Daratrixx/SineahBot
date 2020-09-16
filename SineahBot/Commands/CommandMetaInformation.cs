﻿using SineahBot.Data;
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
            return $@"
**INFORMATION** - ***{character.name}***
> *{character.characterClass.ToString().ToUpper()}* *level **{character.level}*** (*{character.experience}/{ClassProgressionManager.ExperienceForNextLevel(character.level)} exp*)
> *Health* : **{character.health}/{character.maxHealth}**
> *Mana* : **{character.mana}/{character.maxMana}**
> *Spell power* : **{character.GetSpellPower()}** ({(ClassProgressionManager.IsMagicalClass(character.characterClass) ? "2" : "1")} x level)
> *Physical power* : **{character.GetWeaponDamage()}** ({(ClassProgressionManager.IsPhysicalClass(character.characterClass) ? "2" : "1")} x level)
> *Gold*: **{character.gold}**
{(character.experience >= ClassProgressionManager.ExperienceForNextLevel(character.level) ? "*You have enough experience to level up! Type **!level***" : "")}
";
        }
    }
}
