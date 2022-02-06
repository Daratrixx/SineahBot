using SineahBot.Data.Enums;
using SineahBot.Interfaces;

namespace SineahBot.Data
{
    public class AlterationInstance : INamed
    {
        public AlterationType alteration;
        public int remainingTime;

        public string GetName(IAgent agent = null)
        {
            return alteration.ToString();
        }
    }
}
