namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a point in a connected network of nodes.
    /// </summary>
    public class Node
    {
        public string Name { get; set; }
        public List<string> Connections { get; set; } // lista med noder som ligger kopplade med noden.

        /// <summary>
        /// All connected nodes, and their weights.
        /// </summary>
        public List<(Node Node, int Weight)> Edges { get; set; } = new List<(Node, int)>();

        public Node(string name, List<string> connections) // constructor för noder.
        {
            Name = name;
            Connections = connections;
        }

        /// <summary>
        /// Creates an object of Node with <paramref name="name"/>.
        /// </summary>
        /// <param name="name">Name of node</param>
        public Node(string name)
        {
            Name = name;
        }

        /// <summary>
        /// String description of the node and its connections.
        /// </summary>
        /// <returns>Name of node and names and weights of all connected nodes.</returns>
        public override string ToString()
        {
            var nodeToString = $"{Name}:";

            foreach (var (node, weight) in Edges)
            {
                nodeToString += $"\t{node.Name}({weight}) ";
            }

            return nodeToString;
        }
    }
}