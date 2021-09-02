using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift2_Datalogi
{
    public class Node
    {
        public List<(Node node, int weight)> Edges { get; set; } = new List<(Node, int)>();
        public string Name { get; set; }

        public Node(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            var nodeToString = $"{Name}: ";

            foreach (var (node, weight) in Edges)
            {
                nodeToString += $"{node.Name}({weight}) ";
            }

            return nodeToString;
        }
    }
}
