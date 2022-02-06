using SineahBot.Data.Enums;

namespace SineahBot.Interfaces
{
    public interface IDamageable
    {
        void DamageHealth(int damageAmount, DamageType type, INamed source = null);
    }
}
