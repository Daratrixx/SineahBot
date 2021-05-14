using SineahBot.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Behaviours
{
    public class MemoryTracker : IEnumerable<RoomEvent>
    {
        private List<RoomEvent> events = new List<RoomEvent>();
        public IEnumerator<RoomEvent> GetEnumerator()
        {
            return events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return events.GetEnumerator();
        }

        public void Add(RoomEvent e)
        {
            events.Add(e);
        }

        public void Remove(RoomEvent e)
        {
            events.RemoveAll(x => x == e);
        }

        public string ConfirmMemory(string sourceCharacter, RoomEventType type, string targetCharacter, string roomName)
        {
            var typedEvents = events.Where(x => x.type == type);
            if (typedEvents.Count() == 0) return "No.";
            var sourcedEvent = typedEvents.Where(x => string.Equals(x.source.GetName(), sourceCharacter));
            if (sourcedEvent.Count() == 0) return "No.";
            var targeted = sourcedEvent.Where(x => string.Equals(x.target.GetName(), targetCharacter));
            if (targeted.Count() == 0) return "No.";
            var locatedEvent = targeted.Where(x => string.Equals(x.room.GetName(), roomName));
            if (locatedEvent.Count() == 0) return "No.";
            return "Yes.";
        }

        public string ConfirmMemory(string sourceCharacter, RoomEventType type, string roomName)
        {
            var typedEvents = events.Where(x => x.type == type);
            if (typedEvents.Count() == 0) return "No.";
            var sourcedEvent = typedEvents.Where(x => string.Equals(x.source.GetName(), sourceCharacter));
            if (sourcedEvent.Count() == 0) return "No.";
            var locatedEvent = sourcedEvent.Where(x => string.Equals(x.room.GetName(), roomName));
            if (locatedEvent.Count() == 0) return "No.";
            return "Yes.";
        }
    }
}
