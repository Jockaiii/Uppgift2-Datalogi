namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    public class Node
    {
        public string Name { get; set; } // Namnet på nod/busshållplatsen.
        public List<string> Connections { get; set; } // lista med noder som ligger kopplade med noden.
        public List<(Node Node, int Weight)> Edges { get; set; } = new List<(Node, int)>();

        public Node(string name, List<string> connections) // constructor för noder.
        {
            Name = name;
            Connections = connections;
        }

        public Node(string name)
        {
            Name = name;
        }

        /// <summary>
        ///Name of node and names and weights of all connected nodes.
        /// </summary>
        /// <returns></returns>
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