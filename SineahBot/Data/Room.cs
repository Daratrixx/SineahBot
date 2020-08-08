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
        public Room() {
            id = Guid.NewGuid();
        }
        public Room(string roomName)
        {
            id = Guid.NewGuid();
            name = roomName;
        }
        public bool isSpawnRoom { get; set; }
        public string description { get; set; }

        public List<Entity> entities = new List<Entity>();
        public List<Character> characters = new List<Character>();
        public List<Player> players = new List<Player>();
        public List<Item> items = new List<Item>();

        private Dictionary<MoveDirection, Room> directions = new Dictionary<MoveDirection, Room>();

        public Entity FindInRoom(string name)
        {
            return entities.FirstOrDefault(x => x.name.ToLower() == name.ToLower());
        }

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
        public IEnumerable<IAgent> agents
        {
            get { return entities.Where(x => x is IAgent).Select(x => x as IAgent); }
        }

        public string GetShortDescription(IAgent agent = null)
        {
            return $"> **{name}**\n> *{description}*";
        }

        public string GetFullDescription(IAgent agent = null)
        {
            return $"{GetShortDescription(agent)}{String.Concat(observables.Where(x => x != agent).Select(x => " " + x.GetShortDescription(agent)))}";
        }

        public void DescribeAction(string action, IAgent agent = null)
        {
            foreach (var a in agents.Where(x => x != agent))
            {
                a.Message(action);
            }
        }

        public void AddToRoom(Entity entity)
        {
            if (entity is IAgent)
            {
                var agent = entity as IAgent;
                agent.Message(GetFullDescription(agent));
            }
            entity.currentRoomId = this.id;
            // trigger stuff on entity entering
            entities.Add(entity);
        }

        public void RemoveFromRoom(Entity entity)
        {
            entities.Remove(entity);
            entity.currentRoomId = Guid.Empty;
            // trigger stuff on entity leaving
        }
    }
}
