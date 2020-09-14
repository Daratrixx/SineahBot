﻿using SineahBot.Commands;
using SineahBot.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;

namespace SineahBot.Data
{
    public class Room : DataItem, IObservable
    {
        public Room()
        {
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
        public IEnumerable<Character> characters { get { return entities.Where(x => x is Character).Select(x => x as Character); } }
        public IEnumerable<Item> items { get { return entities.Where(x => x is Item).Select(x => x as Item); } }
        public IEnumerable<NPC> npcs { get { return entities.Where(x => x is NPC).Select(x => x as NPC); } }

        private Dictionary<MoveDirection, RoomConnectionState> directions = new Dictionary<MoveDirection, RoomConnectionState>();

        public Entity FindInRoom(string name)
        {
            name = name.ToLower();
            var output = entities.FirstOrDefault(x => x.name.ToLower() == name);
            if (output == null)
                output = items.FirstOrDefault(x => x.alternativeNames.Contains(name));
            if (output == null)
                output = npcs.FirstOrDefault(x => x.alternativeNames.Contains(name));
            return output;
        }

        public IEnumerable<MoveDirection> GetDirections()
        {
            return directions.Keys;
        }

        public void RegisterDirection(MoveDirection direction, RoomConnectionState roomConnection)
        {
            if (directions.ContainsKey(direction)) throw new Exception($"Direction duplicate for room {name}=>{direction}=>{roomConnection.toRoom.name} X {directions[direction].toRoom.name}");
            directions[direction] = roomConnection;
        }
        public RoomConnectionState GetRoomConnectionInDirection(MoveDirection direction)
        {
            if (!directions.ContainsKey(direction)) throw new Exception($@"Direction ""{direction}"" not defined for room {name}");
            return directions[direction];
        }
        public bool IsValidDirection(MoveDirection direction)
        {
            return directions.ContainsKey(direction);
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
            return $"{GetShortDescription(agent)}{(observables.Count() > 0 ? $"\n> \\> {String.Concat(observables.Where(x => x != agent).Select(x => " " + x.GetShortDescription(agent)))}" : "")}";
        }

        public void DescribeAction(string action, params IAgent[] agent)
        {
            foreach (var a in agents.Where(x => !agent.Contains(x)))
            {
                a.Message(action);
            }
        }

        public void DescribeActionNow(string action, params IAgent[] agent)
        {
            foreach (var a in agents.Where(x => !agent.Contains(x)))
            {
                a.Message(action, true);
            }
        }

        public void AddToRoom(Entity entity, bool feedback = true)
        {
            if (entity is NPC)
                DescribeActionNow($"{entity.GetName()} has entered the room.", entity as IAgent);
            else if (entity is Character)
                DescribeAction($"{entity.GetName()} has entered the room.", entity as IAgent);
            if (entity is IAgent && feedback)
            {
                var agent = entity as IAgent;
                agent.Message(GetFullDescription(agent));
            }
            entity.currentRoomId = this.id;
            // trigger stuff on entity entering
            entities.Add(entity);
            if (entity is NPC)
            {
                var npc = entity as NPC;
                if (npc.idSpawnRoom == Guid.Empty)
                    npc.idSpawnRoom = this.id;
            }
        }

        public void RemoveFromRoom(Entity entity, bool feedback = true)
        {
            entities.Remove(entity);
            entity.currentRoomId = Guid.Empty;
            if (feedback)
            {
                if (entity is NPC)
                    DescribeActionNow($"{entity.name} has left the room.", entity as IAgent);
                else if (entity is Character)
                    DescribeAction($"{entity.name} has left the room.", entity as IAgent);
            }
            // trigger stuff on entity leaving
        }

        public string GetName(IAgent agent = null)
        {
            return name;
        }
    }
}
