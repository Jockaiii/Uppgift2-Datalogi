namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    static public class Seeder
    {
        public static List<(string nodeName, List<string> connections)> nodeSeedList = new List<(string, List<string> connections)>
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

        public static List<(string name, int weight)> edgeSeedList = new List<(string, int)>
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

        /// <summary>
        /// Get seed data for nodes and edges.
        /// </summary>
        /// <returns>List of nodes connected by edges.</returns>
        static public List<Node> Data()
        {
            List<Node> nodes = new List<Node>();

            // declare nodes
            var nodeA = new Node("A");
            var nodeB = new Node("B");
            var nodeC = new Node("C");
            var nodeD = new Node("D");
            var nodeE = new Node("E");
            var nodeF = new Node("F");
            var nodeG = new Node("G");
            var nodeH = new Node("H");
            var nodeI = new Node("I");
            var nodeJ = new Node("J");
            var nodeHindoes = new Node("Hindås");

            // add nodes
            nodes.Add(nodeA);
            nodes.Add(nodeB);
            nodes.Add(nodeC);
            nodes.Add(nodeD);
            nodes.Add(nodeE);
            nodes.Add(nodeF);
            nodes.Add(nodeG);
            nodes.Add(nodeH);
            nodes.Add(nodeI);
            nodes.Add(nodeJ);
            nodes.Add(nodeHindoes);

            // add edges for node A
            nodeA.Edges.Add((nodeB, 4));
            nodeA.Edges.Add((nodeC, 7));
            nodeA.Edges.Add((nodeE, 7));
            nodeA.Edges.Add((nodeE, 7));
            nodeA.Edges.Add((nodeHindoes, 31));

            // add edges for node B
            nodeB.Edges.Add((nodeA, 4));
            nodeB.Edges.Add((nodeC, 3));
            nodeB.Edges.Add((nodeH, 5));
            nodeB.Edges.Add((nodeD, 12));

            // add edges for node C
            nodeC.Edges.Add((nodeB, 3));
            nodeC.Edges.Add((nodeI, 12));
            nodeC.Edges.Add((nodeG, 4));
            nodeC.Edges.Add((nodeA, 7));

            // add edges for node D
            nodeD.Edges.Add((nodeI, 3));
            nodeD.Edges.Add((nodeH, 7));
            nodeD.Edges.Add((nodeB, 12));

            // add edges for node E
            nodeE.Edges.Add((nodeA, 7));
            nodeE.Edges.Add((nodeG, 5));
            nodeE.Edges.Add((nodeF, 3));

            // add edges for node F
            nodeF.Edges.Add((nodeE, 3));
            nodeF.Edges.Add((nodeG, 5));

            // add edges for node G
            nodeG.Edges.Add((nodeC, 4));
            nodeG.Edges.Add((nodeH, 8));
            nodeG.Edges.Add((nodeJ, 8));
            nodeG.Edges.Add((nodeF, 5));
            nodeG.Edges.Add((nodeE, 5));

            // add edges for node H
            nodeH.Edges.Add((nodeD, 7));
            nodeH.Edges.Add((nodeJ, 9));
            nodeH.Edges.Add((nodeG, 8));
            nodeH.Edges.Add((nodeB, 5));

            // add edges for node I
            nodeI.Edges.Add((nodeD, 3));
            nodeI.Edges.Add((nodeJ, 7));
            nodeI.Edges.Add((nodeG, 13));
            nodeI.Edges.Add((nodeC, 12));

            // add edges for node J
            nodeJ.Edges.Add((nodeI, 7));
            nodeJ.Edges.Add((nodeG, 8));
            nodeJ.Edges.Add((nodeH, 9));
            nodeJ.Edges.Add((nodeHindoes, 2));

            // add edges for node Hindås
            nodeHindoes.Edges.Add((nodeA, 31));
            nodeHindoes.Edges.Add((nodeJ, 2));

            return nodes;
        }
    }
}