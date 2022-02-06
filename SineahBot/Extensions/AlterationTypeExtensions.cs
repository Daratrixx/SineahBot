using SineahBot.Data.Enums;

namespace SineahBot.Extensions
{
    public static class AlterationTypeExtensions
    {
        public static string GetDescription(this AlterationType alteration)
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
    }
}
