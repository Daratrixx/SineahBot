using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Alteration : INamed
    {
        public AlterationType alteration;
        public int remainingTime;

        public static string GetAlterationDescription(AlterationType alteration)
        {
            switch (alteration)
            {
                case AlterationType.Burning: return $"You will take a lot of damage over time and you have a chance to get **{AlterationType.Burnt}**";
                case AlterationType.Burnt: return $"Healing from every source is halved.";
                case AlterationType.Poisoned: return $"Mana regeneration is halved.";
                case AlterationType.Sickness: return $"You will take a little bit of damage over time.";
                case AlterationType.Weakened: return $"Your physical attacks will be half as powerful.";
                case AlterationType.Blind: return $"You won't be able to see anything.";
                case AlterationType.Deaf: return $"You won't be able to hear anything.";
                case AlterationType.Frenzied: return $"You will attack randomly as soon as you are able to.";
                case AlterationType.Taunted: return $"You will attack the character that taunted you as often as possible.";
                case AlterationType.Stunned: return $"You won't be able to act or perceive anything for a while.";
                case AlterationType.Invisible: return $"Other characters won't be able to see you.";
                case AlterationType.Shrouded: return $"Other characters won't have a clear view of you.";
                case AlterationType.Warded: return $"You will receive halved magic damage.";
                case AlterationType.Amplified: return $"Your spells will be half more powerful.";
                case AlterationType.Hardened: return $"you will receive halved physical damage.";
                case AlterationType.Empowered: return $"Your attacks will be half more powerful.";
                default: return "Unknown alteration, no description.";
            }
        }

        public string GetName(IAgent agent = null)
        {
            return alteration.ToString();
        }
    }

    public enum AlterationType
    {
        Burning, // get damage over time
        Burnt, // get reduced healing
        Poisoned, // get reduced mana regen
        Sickness, // get damage over time
        Weakened, // deal reduced damamge
        Blind, // can't see other entities, room descriptions, or directions
        Deaf, // can't heat character talking, or access being locked/unlocked
        Frenzied, // will attack a random target every few seconds
        Taunted, // will attack the taunting target every few seconds
        Sleeping, // can't perceive anything nor act, regenerates faster
        Stunned, // can't perceive anything nor act
        Invisible, // can't be seen by other characters
        Shrouded, // hides identity
        Warded, // get reduced magic damage and heal
        Amplified, // deal increased magic damage and heal
        Hardened, // get reduced physical damage
        Empowered, // deal increased physical damage
    }
}
