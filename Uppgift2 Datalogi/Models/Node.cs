namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a node (or point) in a network and it's connected nodes.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Name property of object Node
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// All connected nodes, and their weights.
        /// </summary>
        public List<(Node Node, int Weight)> Edges { get; set; } = new List<(Node, int)>();

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