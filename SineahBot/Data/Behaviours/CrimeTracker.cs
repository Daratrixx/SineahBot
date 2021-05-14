using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public class CrimeTracker : IEnumerable<Crime>
    {
        private List<Crime> crimes = new List<Crime>();
        public IEnumerator<Crime> GetEnumerator()
        {
            return crimes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return crimes.GetEnumerator();
        }

        public void Add(Crime crime)
        {
            crimes.Add(crime);
        }

        public void Remove(Crime crime)
        {
            crimes.RemoveAll(x => x == crime);
        }

        public IEnumerable<Crime> FindCrimesForPerpetrator(string perpetrator)
        {
            return crimes.Where(x => string.Equals(x.perpetrator, perpetrator, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Crime> FindCrimesForVictim(string victim)
        {
            return crimes.Where(x => string.Equals(x.victim, victim, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Crime> FindCrimesForRoom(Room room)
        {
            return FindCrimesForRoom(room.name);
        }

        public IEnumerable<Crime> FindCrimesForRoom(string roomName)
        {
            return crimes.Where(x => string.Equals(x.roomName, roomName, StringComparison.OrdinalIgnoreCase));
        }

        public void TickAffectations()
        {
            foreach (var crime in crimes.Where(x => x.solved == false))
            {
                crime.TickAffectations();
            }
        }
    }

    public class Crime
    {
        public string perpetrator;
        public string victim;
        public string roomName;
        public bool perpetratorConfirmed;
        public bool victimConfirmed;
        public bool solved;

        private Dictionary<NPC, int> affectedNPC = new Dictionary<NPC, int>();

        public void AffectNPC(NPC npc)
        {
            affectedNPC[npc] = 0;
        }

        public void TickAffectations()
        {
            foreach (var affectation in affectedNPC.Keys)
            {
                ++affectedNPC[affectation];
            }
        }
    }
}
