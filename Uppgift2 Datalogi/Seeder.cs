using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift2_Datalogi
{
    static class Seeder
    {
        public static List<(string nodeName, List<string> connections)> nodeSeedList = new List<(string, List<string>connections)>
        {
            { ("A", new List<string> {"B", "C", "E" }) },
            { ("B", new List<string> {"A", "C", "D", "H" }) },
            { ("C", new List<string> {"A", "B", "G", "I" }) },
            { ("D", new List<string> {"B", "H", "I" }) },
            { ("E", new List<string> {"A", "F", "G" }) },
            { ("F", new List<string> {"E", "G" }) },
            { ("G", new List<string> {"C", "E", "F", "I", "J" }) },
            { ("H", new List<string> {"B", "D", "G", "J" }) },
            { ("I", new List<string> {"C", "D", "G", "J" }) },
            { ("J", new List<string> {"G", "H", "J" }) },
        };

        public static List<(string name , int weight)> edgeSeedList = new List<(string, int)>
        {
            { ("A-B", 4) },
            { ("A-C", 7) },
            { ("A-E", 7) },
            { ("B-C", 3) },
            { ("B-H", 5) },
            { ("B-D", 12) },
            { ("C-I", 12) },
            { ("C-G", 4) },
            { ("D-H", 7) },
            { ("D-I", 3) },
            { ("E-G", 5) },
            { ("E-F", 3) },
            { ("F-G", 5) },
            { ("G-I", 13) },
            { ("G-J", 8) },
            { ("G-H", 8) },
            { ("H-J", 9) },
            { ("I-J", 7) },
        };
        public static void NodeSeeder()
        {
            foreach (var (nodeName, connections) in nodeSeedList)
                RouteCity.Nodes.Add(new Node(nodeName, connections));
        }

        public static void EdgeSeeder()
        {
            foreach (var (name, weight) in edgeSeedList)
                RouteCity.Edges.Add(new Edge(name, weight));
        }
    }
}