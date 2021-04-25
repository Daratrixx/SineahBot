using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SineahBot.Data.Path
{
    public class Graph<NodeType, NodeData> where NodeType : Node<NodeData> where NodeData : class, INeighbor<NodeData>
    {
        public Graph(IEnumerable<NodeData> data, bool threaded = true)
        {
            nodes = data.Select((nodeData, id) => new Node<NodeType, NodeData>() { id = id, nodeData = nodeData, graph = this }).ToArray();
            distances = new int[nodes.Length, nodes.Length];
            for (int i = 0; i < nodes.Length; ++i)
            {
                for (int j = 0; j < nodes.Length; ++j)
                {
                    distances[i, j] = i == j ? 0 : int.MaxValue;
                }
            }
            if (threaded)
                GenerateThreaded();
            else
                Generate();
        }
        private void Generate()
        {
            foreach (var n in nodes)
            {
                BucketFillNode(n);
            }
        }
        private void GenerateThreaded()
        {
            List<System.Threading.Thread> threads = new List<System.Threading.Thread>();
            // create threads for each nodes
            foreach (var n in nodes)
            {
                var thread = new System.Threading.Thread(() => { BucketFillNode(n); });
                threads.Add(thread);
            }
            // start threads
            foreach (var thread in threads)
            {
                thread.Start();
            }
            // wait for threads to finish
            foreach (var thread in threads)
            {
                thread.Join();
            }

        }

        private Node<NodeType, NodeData>[] nodes;
        private int[,] distances;
        public int GetDistance(Node<NodeData> a, Node<NodeData> b)
        {
            if (!nodes.Contains(a) || !nodes.Contains(b))
                return -1;
            return distances[a.id, b.id];
        }
        public IEnumerable<Node<NodeType, NodeData>> GetNeighbors(Node<NodeType, NodeData> node)
        {
            return nodes.Where(x => distances[node.id, x.id] == 1);
        }
        public Node<NodeType, NodeData> GetNodeForData(NodeData data)
        {
            return nodes.FirstOrDefault(x => x.nodeData == data);
        }

        private void BucketFillNode(Node<NodeType, NodeData> n)
        {
            var doneNodes = new List<Node<NodeType, NodeData>>();
            doneNodes.Add(n);
            var remainingNodes = nodes.ToList();
            remainingNodes.Remove(n);
            int depth = 1;
            while (remainingNodes.Count > 0)
            {
                var neighbors = new List<Node<NodeType, NodeData>>();
                foreach (var node in remainingNodes)
                {
                    if (doneNodes.Where(x => x.nodeData.IsNeighbor(node.nodeData)).Count() > 0)
                    {
                        neighbors.Add(node);
                    }
                }
                if (neighbors.Count() == 0)
                    break; // can't find new neighbors, stop the loop
                foreach (var node in neighbors)
                {
                    distances[n.id, node.id] = Math.Min(distances[n.id, node.id], depth);
                    //distances[node.id, n.id] = Math.Min(distances[node.id, n.id], depth);
                    remainingNodes.Remove(node);
                    doneNodes.Add(node);
                }
                ++depth;
            }
        }
    }

    public class Node<NodeData> where NodeData : class, INeighbor<NodeData>
    {
        public int id;
        public NodeData nodeData;

        public override string ToString()
        {
            return $@"Node {id} ({nodeData})";
        }
    }

    public class Node<NodeType, NodeData> : Node<NodeData> where NodeType : Node<NodeData> where NodeData : class, INeighbor<NodeData>
    {
        public Graph<NodeType, NodeData> graph;
        public List<Node<NodeType, NodeData>> neightbours;
    }
}
