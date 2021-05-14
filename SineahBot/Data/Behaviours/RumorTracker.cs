using SineahBot.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public class RumorTracker : IEnumerable<Rumor>
    {
        private List<Rumor> rumors = new List<Rumor>();
        public IEnumerator<Rumor> GetEnumerator()
        {
            return rumors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return rumors.GetEnumerator();
        }

        public void Add(Rumor rumor)
        {
            rumors.Add(rumor);
        }

        public void Remove(Rumor rumor)
        {
            rumors.RemoveAll(x => x == rumor);
        }

        public int Count { get{ return rumors.Count; } }
    }
    public class Rumor : BehaviourMission
    {
        public Rumor(RoomEvent sourceEvent, string rumorText) : base(sourceEvent) { this.rumorText = rumorText; }
        public string rumorText;
        public List<Character> spreadTo = new List<Character>();
        public bool reported;

        public override string ToString()
        {
            return @$"Rumor: ""{rumorText}"" (from {sourceEvent?.source})";
        }
    }
}
