using SineahBot.Data.Enums;
using System.Collections.Generic;

namespace SineahBot.Data
{
    public class Faction
    {
        public Dictionary<Faction, FactionRelation> relations = new Dictionary<Faction, FactionRelation>();

        public FactionRelation defaultRelation = FactionRelation.Neutral;
    }
}
