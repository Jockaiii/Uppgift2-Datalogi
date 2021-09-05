namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    public class Node
    {
        public string Name { get; set; } // Namnet på nod/busshållplatsen.
        public List<string> Connections { get; set; } // lista med noder som ligger kopplade med noden.
        public List<(Node node, int weight)> Edges { get; set; } = new List<(Node, int)>();

        public Node(string name, List<string> connections) // constructor för noder.
        {
            Name = name;
            Connections = connections;
        }

        public Node(string name)
        {
            Name = name;
        }

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