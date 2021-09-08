namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

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
                // TODO: compare paths to find shortest

                // Don't visit a node twice.
                if (!path.visited.Contains(edge.node))
                {
                    // Add node and cost to path, and move in to node.
                    path.visited.Add(edge.node);
                    path.cost += edge.weight;
                    
                    var previousCost = path.cost;

                    FindPath(edge.node, end, edge.weight, path);

                    

                    return FindPath(edge.node, end, edge.weight, path);
                }

                //if (path.cost > previousCost)
                //{
                //    path.visited.Remove(edge.node);
                //    path.cost -= edge.weight;
                //}
            }

            // Dead end, remove edge from path and return.
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

        public static void PathHandler() // todo: Lägg till funktionalitet för att hantera om användaren vill ha ett delmål. Refactoring & optimisering?
        {
            if (InputOutput.UserNodes.Count == 2) // om användaren har valt 2 nodes
            {
                var startNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[0]); // Objekt som representerar start och slutnoderna som användaren anget.
                var endNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[1]);

                if (!CheckForEndNodeFrom1NodeAway(startNode, endNode)) // Om slutnoden ligger brevid startnoden
                    if (!CheckEndNodeFrom2NodesAway(startNode, endNode)) // Om slutnoden inte ligger brevid startnoden
                        CheckEndNodeFrom3NodesAway(startNode, endNode); // Kollar alla noder som ligger 3 noder ifrån startnoden

                //if (!CheckForEndNodeFrom1NodeAway(startNode, endNode))
                //    PathFinders(startNode, endNode);
            }
            else// om användaren har valt 2 nodes med en mellanlandning
            {
                var startNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[0]);
                var stopNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[1]);
                var endNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[2]);
            }

            if (Paths.Count > 1) // Om algoritmen funnit mer än 1 väg till slutnoden.
                PathChecker();
        }

        /// <summary>
        /// Metod som söker efter slutnoden runtom startnoden. Samt lägger till eventuellt funnen väg i List<Path> Paths
        /// </summary>
        /// <param name="startNode">Objekt som representerar startnoden som användren har valt</param>
        /// <param name="endNode">Objekt som representerar slutnoden som användaren har valt</param>
        /// <returns></returns>
        public static bool CheckForEndNodeFrom1NodeAway(Node startNode, Node endNode)
        {
            if (startNode.Connections.Contains(endNode.Name)) // Kollar noderna runtom startnoden.
            {
                int totalWeight = RouteCity.Edges.Find(f => f.Connections.Contains(startNode.Name) && f.Connections.Contains(endNode.Name)).Weight; // Lägger ihop sträckorna till slutnoden
                Paths.Add(new Models.Path(totalWeight)); // Lägger till en ny Path() i List<Path> Paths
                return true; // Retunerar true så att algoritmen inte fortsätter att leta efter slutnod längre bort.
            }
            return false; // Retunerar false om metoden inte fann någon path till slutnoden så att algoritmen kan fortsätta leta efter slutnoden längre bort
        }

        /// <summary>
        /// Metod som söker efter slutnoden 3 noder bort från startnoden. Samt lägger till eventuellt funnen väg i List<Paths> Path
        /// </summary>
        /// <param name="startNode">Objekt som representerar startnoden som användren har valt</param>
        /// <param name="endNode">Objekt som representerar slutnoden som användaren har valt</param>
        /// <returns></returns>
        public static bool CheckEndNodeFrom2NodesAway(Node startNode, Node endNode) // O(n^2)
        {
            foreach (var nodeName in startNode.Connections) // Kollar alla noder som ligger brevid startnodens connections.
                foreach (var nodeName2 in RouteCity.Nodes.Find(f => f.Name == nodeName).Connections)
                    if (nodeName2 == endNode.Name)
                    {
                        List<string> visitedNodes = new List<string>();
                        int totalWeight = RouteCity.Edges.Find(f => f.Connections.Contains(nodeName) && f.Connections.Contains(nodeName2)).Weight + RouteCity.Edges.Find(f => f.Connections.Contains(startNode.Name) && f.Connections.Contains(nodeName)).Weight;
                        visitedNodes.Add(nodeName); // Den mittersta nodens namn i pathen (första och sista finns i InputOutput.UserNodes

                        Paths.Add(new Models.Path(totalWeight, visitedNodes));
                        return true;
                    }
            return false;
        }

        /// <summary>
        /// Metod som söker efter slutnoden 3 noder bort från startnoden. Samt lägger till eventuellt funnen väg i List<Path> Paths
        /// </summary>
        /// <param name="startNode">Objekt som representerar startnoden som användren har valt</param>
        /// <param name="endNode">Objekt som representerar slutnoden som användaren har valt</param>
        public static void CheckEndNodeFrom3NodesAway(Node startNode, Node endNode) // O(n^3)
        {
            foreach (var nodeName in startNode.Connections) // Kollar alla noder som ligger brevid startnodens connections Connections.
                foreach (var nodeName2 in RouteCity.Nodes.Find(f => f.Name == nodeName).Connections)
                    foreach (var nodeName3 in RouteCity.Nodes.Find(f => f.Name == nodeName2).Connections)
                        if (nodeName3 == endNode.Name)
                        {
                            List<string> visitedNodes = new List<string>();

                            int totalWeight = RouteCity.Edges.Find(f => f.Connections.Contains(nodeName) && f.Connections.Contains(nodeName2)).Weight + RouteCity.Edges.Find(f => f.Connections.Contains(nodeName2) && f.Connections.Contains(nodeName3)).Weight + RouteCity.Edges.Find(f => f.Connections.Contains(startNode.Name) && f.Connections.Contains(nodeName)).Weight;
                            visitedNodes.Add(nodeName);
                            visitedNodes.Add(nodeName2);

                            Paths.Add(new Models.Path(totalWeight, visitedNodes));
                        }
        }

        /// <summary>
        /// Metod som itererar igenom List<Path> Paths och jämför sträckorna mellan de olika vägarna som funnits. Och tar bort alla förutom den kortaste sträckan.
        /// </summary>
        public static void PathChecker()
        {
            for (int i = 0; i < Paths.Count; i++)
                for (int j = 0; j < Paths.Count; j++)
                    if (Paths[i].Weight < Paths[j].Weight)
                        Paths.RemoveAt(j);
        }

        //public static List<string> visitedNodes2 { get; set; } = new List<string>();
        //public static int Count { get; set; } = -1;
        //public static int TotalWeight2 { get; set; } = 0;
        //public static void PathFinders(Node startNode, Node endNode) // rekursions version av algortimen.
        //{
        //    foreach (var nodeName in startNode.Connections)
        //        if (nodeName == endNode.Name)
        //        {
        //            TotalWeight2 += RouteCity.Edges.Find(f => f.Connections.Contains(startNode.Name) && f.Connections.Contains(endNode.Name)).Weight;
        //            visitedNodes2.Add(nodeName);

        //            Paths.Add(new Models.Path(TotalWeight2, visitedNodes2));
        //            Count = 0;
        //            TotalWeight2 = 0;
        //        }
        //    foreach (var nodeName2 in startNode.Connections)
        //    {
        //        PathFinders(RouteCity.Nodes.Find(f => f.Name == nodeName2), endNode);
        //    }
        //    TotalWeight2 += RouteCity.Edges.Find(f => f.Connections.Contains(startNode.Name) && f.Connections.Contains(startNode.Connections[0])).Weight;
        //    visitedNodes2.Add(startNode.Name);
        //    PathFinders(RouteCity.Nodes.Find(f => f.Name == startNode.Connections[Count++]), endNode);
        //}
    }
}