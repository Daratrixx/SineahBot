﻿using SineahBot.Commands;
using SineahBot.Data;
using SineahBot.Data.Path;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Tools
{
    public class RoomNode : Node<RoomNode, Room>
    {

    }
    public class RoomGraph : Graph<RoomNode, Room>
    {
        public RoomGraph(IEnumerable<Room> data, bool threaded = true) : base(data, threaded)
        {
        }
    }
    public static class PathBuilder
    {
        private static Dictionary<Room, RoomGraph> roomGraphs = new Dictionary<Room, RoomGraph>();
        public static RoomGraph SineahGraph;

        public static void BuildGraphs()
        {
            SineahGraph = BuildGraph(SineahBot.Data.World.Sineah.rooms, false);
        }

        private static RoomGraph BuildGraph(IEnumerable<Room> data, bool threaded = true)
        {
            DateTime buildingStart = DateTime.Now;
            var output = new RoomGraph(data, threaded);
            foreach (var room in data)
            {
                roomGraphs[room] = output;
            }
            DateTime buildingEnd = DateTime.Now;
            Logging.Log($"PathBuilder>Graph generated in {buildingEnd - buildingStart} ({(threaded? "threaded" : "not threaded")})");
            return output;
        }

        public static MoveDirection GetNextMove(Room from, Room to)
        {
            var graph = roomGraphs[from];
            if (graph != roomGraphs[to])
                return MoveDirection.None;
            var toNode = graph.GetNodeForData(to);
            if (toNode == null)
                return MoveDirection.None;

            var targetRooms = from.GetNeighboringRooms()
            .Select(x => new { room = x, node = graph.GetNodeForData(x) })
            .Where(x => x.node != null)
            .Select(x => new { x.room, x.node, d = graph.GetDistance(x.node, toNode) })
            .Where(x => x.d >= 0)
            .OrderBy(x => x.d).ToArray();
            var targetRoom = targetRooms
             .FirstOrDefault();

            if (targetRoom == null)
                return MoveDirection.None;

            return from.GetDirectionToRoom(targetRoom.room);
        }
    }
}
