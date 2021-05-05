using SineahBot.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SineahBot.Tools
{
    public static class FactionManager
    {
        public static void InitFactions()
        {
            RegisterFaction(Sineah);
            RegisterFaction(Undead);
            RegisterFaction(Beasts);
            RegisterFaction(AllHostile);
        }

        public static Faction Sineah = new Faction() { defaultRelation = FactionRelation.Neutral };
        public static Faction Undead = new Faction() { defaultRelation = FactionRelation.Negative };
        public static Faction Beasts = new Faction() { defaultRelation = FactionRelation.Negative };
        public static Faction AllHostile = new Faction() { defaultRelation = FactionRelation.Hostile };

        private static List<Faction> factions = new List<Faction>();

        public static void RegisterFaction(Faction faction)
        {
            factions.Add(faction);
        }

        public static void SetFactionRelation(Faction a, Faction b, FactionRelation relation)
        {
            a.relations[b] = relation;
            b.relations[a] = relation;
        }

        public static FactionRelation GetFactionRelation(Faction a, Faction b)
        {
            if (a == null || b == null)
                return FactionRelation.Hostile;
            if (a.relations.TryGetValue(b, out var relation))
                return relation;
            return (FactionRelation)Math.Max((int)a.defaultRelation, (int)b.defaultRelation);
        }

        public static Faction CreatePlayerRepFaction()
        {
            var output = new Faction() { defaultRelation = FactionRelation.Good };
            foreach (var faction in factions)
            {
                SetFactionRelation(faction, output, faction.defaultRelation);
            }
            return output;
        }
    }
}
