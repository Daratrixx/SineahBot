using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Data
{
    public class Faction
    {
        public Dictionary<Faction, FactionRelation> relations = new Dictionary<Faction, FactionRelation>();

        public FactionRelation defaultRelation = FactionRelation.Neutral;
    }


    public enum FactionRelation
    {
        Hostile,
        Negative,
        Neutral,
        Good,
        Friendly,
    }
}
