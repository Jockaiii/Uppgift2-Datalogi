namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PathFinder
    {
        public static (List<Node> visited, int cost, bool found) ShortestPath(Node start, Node end)
        {
            var paths = new List<(List<Node> visited, int cost, bool found)>();

            // TODO: out of all possible paths which is fastest?

            // Get a path...
            var (visited, cost, found) = FindPath(start, end, 0, (new List<Node>() { start }, 0, false));

            return (visited, cost, found);
        }

        private static (List<Node> visited, int cost, bool found) FindPath(Node current, Node end, int cost, (List<Node> visited, int cost, bool found) path)
        {
            if (current.Name == end.Name)
            {
                // End node found.
                path.found = true;
                return path;
            }
            foreach (var edge in current.Edges)
            {
                // TODO: 

                if (!path.visited.Contains(edge.node))
                {
                    // Add node and cost to path, and move in to node.
                    path.visited.Add(edge.node);
                    path.cost += edge.weight;
                    return FindPath(edge.node, end, edge.weight, path); // TODO: turnary on path.found possible?
                }
            }

            // Dead end.
            // Remove edge from path and return.
            path.visited.RemoveAt(path.visited.Count - 1);
            path.cost -= cost;
            return path;
        }

        public static int ShortestPath(Node start, Node visit, Node end)
        {
            throw new NotImplementedException();
        }

//---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static List<Models.Path> Paths { get; set; } = new List<Models.Path>();

        public static void PathHandler() // todo: Hantera krasher om användaren inte inputar i alfabetisk ordning
        {                                // Lägg till funktionalitet för att hantera om användaren vill ha ett delmål. Refactoring & optimisering?
            if (InputOutput.UserNodes.Count == 2) // om användaren har valt 2 nodes
            {
                var startNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[0]);
                var endNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[1]);

                if (!CheckForEndNodeFrom1NodeAway(startNode, endNode)) // Om slutnoden ligger brevid startnoden
                    if (!CheckEndNodeFrom2NodesAway(startNode, endNode)) // Om slutnoden inte ligger brevid startnoden
                        CheckEndNodeFrom3NodesAway(startNode, endNode); // Kollar alla noder som ligger 3 noder ifrån startnoden
            }
            else if (InputOutput.UserNodes.Count == 3)// om användaren har valt 2 nodes med en mellanlandning
            {
                var startNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[0]);
                var stopNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[1]);
                var endNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[2]);
            }
            else
            {
                InputOutput.UserNodes.Reverse(); // flippar ordningen av userinputs så att dom hamnar i bokstavsordning så algoritmen kan hitta path.
                InputOutput.ReverseResultOutput = true;
                PathHandler(); // rekurserar PathHandler() med den nya ordningen av UserNodes
            }

            if (Paths.Count > 1)
                PathChecker();
        }

        public static bool CheckForEndNodeFrom1NodeAway(Node startNode, Node endNode)
        {
            if (startNode.Connections.Contains(endNode.Name))
            {
                int totalWeight = RouteCity.Edges.First(f => f.Name == startNode.Name + endNode.Name).Weight; // skickar tillbaka vikten av sträckan
                Paths.Add(new Models.Path(totalWeight));
                return true;
            }
            return false;
        }

        public static bool CheckEndNodeFrom2NodesAway(Node startNode, Node endNode)
        {
            foreach (var nodeName in startNode.Connections) // Kollar alla noder som ligger brevid startnodens connections.
                foreach (var nodeName2 in RouteCity.Nodes.Find(f => f.Name == nodeName).Connections)
                    if (nodeName2 == endNode.Name)
                    {
                        List<string> visitedNodes = new List<string>();
                        string edgeName = nodeName + nodeName2;

                        int totalWeight = RouteCity.Edges.Find(f => f.Name == edgeName).Weight + RouteCity.Edges.Find(f => f.Name == startNode.Name + nodeName).Weight;
                        visitedNodes.Add(nodeName); // Den mittersta nodens namn i pathen (första och sista finns i InputOutput.UserNodes

                        Paths.Add(new Models.Path(totalWeight, visitedNodes));
                        return true;
                    }
                return false;
        }

        public static void CheckEndNodeFrom3NodesAway(Node startNode, Node endNode)
        {
            foreach (var nodeName in startNode.Connections) // Kollar alla noder som ligger brevid startnodens connections Connections.
                foreach (var nodeName2 in RouteCity.Nodes.Find(f => f.Name == nodeName).Connections)
                    foreach (var nodeName3 in RouteCity.Nodes.Find(f => f.Name == nodeName2).Connections)
                        if (nodeName3 == endNode.Name)
                        {
                            List<string> visitedNodes = new List<string>();
                            string edgeName1 = nodeName + nodeName2;
                            string edgeName2 = nodeName2 + nodeName3;

                            int totalWeight = RouteCity.Edges.Find(f => f.Name == edgeName1).Weight + RouteCity.Edges.Find(f => f.Name == edgeName2).Weight + RouteCity.Edges.Find(f => f.Name == startNode.Name + nodeName).Weight;
                            visitedNodes.Add(nodeName);
                            visitedNodes.Add(nodeName2);

                            Paths.Add(new Models.Path(totalWeight, visitedNodes));
                        }
        }

        public static void PathChecker()
        {
            for (int i = 0; i < Paths.Count; i++) // Itererar igenom vägarna i Paths och tar bort dom som är längre
                for (int j = 0; j < Paths.Count; j++)
                    if (Paths[i].Weight < Paths[j].Weight)
                        Paths.RemoveAt(j);
        }
    }
}