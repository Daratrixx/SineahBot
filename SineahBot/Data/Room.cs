using SineahBot.Commands;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Room : DataItem, IObservable
    {

        public bool isSpawnRoom { get; set; }
        public string description { get; set; }

        public List<Entity> entities = new List<Entity>();
        public List<Character> characters = new List<Character>();
        public List<Player> players = new List<Player>();
        public List<Item> items = new List<Item>();

        private Dictionary<MoveDirection, Room> directions = new Dictionary<MoveDirection, Room>();

        public IEnumerable<MoveDirection> GetDirections()
        {
            return directions.Keys;
        }
        public void RegisterDirection(MoveDirection direction, Room room)
        {
            if (directions.ContainsKey(direction)) throw new Exception($"Direction duplicate for room {this.id}=>{direction}=>{room.id}X{directions[direction].id}");
            directions[direction] = room;
        }
        public Room GetRoomInDirection(MoveDirection direction)
        {
            if (!directions.ContainsKey(direction)) throw new Exception($@"Direction {direction} not defined for room {id}");
            return directions[direction];
        }

        public IEnumerable<IObservable> observables
        {
            get { return entities.Where(x => x is IObservable).Select(x => x as IObservable); }
        }
        public IEnumerable<IAttackable> attackables
        {
            get { return entities.Where(x => x is IAttackable).Select(x => x as IAttackable); }
        }
        public IEnumerable<IPickable> pickables
        {
            get { return entities.Where(x => x is IPickable).Select(x => x as IPickable); }
        }
        public IEnumerable<IObserver> observers
        {
            get { return entities.Where(x => x is IObserver).Select(x => x as IObserver); }
        }

        public string GetNormalDescription()
        {
            return description + String.Join(" ", observables.Select(x => x.GetNormalDescription()));
        }

        public string GetSearchDescription()
        {
            throw new NotImplementedException();
        }

        public bool IsHidden(int detectionValue)
        {
            return false;
        }

        public void OnObserved(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public void OnSearched(IAgent agent)
        {
            throw new NotImplementedException();
        }

        public void AddToRoom(Entity entity)
        {
            if (entity is IAgent)
            {
                var agent = entity as IAgent;
                agent.Message(GetNormalDescription());
            }
            entity.currentRoomId = this.id;
            entities.Add(entity);
            // trigger stuff on entity entering
        }

        public void RemoveFromRoom(Entity entity)
        {
            entities.Remove(entity);
            entity.currentRoomId = Guid.Empty;
            // trigger stuff on entity leaving
        }
    }
}
