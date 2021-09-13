namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public static class PathFinder
    {
        // TODO: compare paths to find shortest
        public static (List<Node> visited, int cost, bool found) ShortestPath(Node start, Node end)
        {
            // Get a path...
            var (visited, cost, found) = FindPath(start, end, 0, (new List<Node>() { start }, 0, false));

            return (visited, cost, found);
        }

        /// <summary>
        /// Implementing Dijkstras algorithm the shortest path between <paramref name="start"/> and <paramref name="end"/> is found.
        /// </summary>
        /// <param name="start">From</param>
        /// <param name="end">To</param>
        /// <returns></returns>
        public static (List<Node> visited, int cost, bool found) DijkstrasShortestPath(Node start, Node end)
        {
            var minCostsToStart = DijkstrasCosts(start, end);
            var found = true; // TODO

            //var shortestPath = new List<Node>();

            var endNodeCost = minCostsToStart.Find((nc) => nc.Node == end);
            var cost = endNodeCost.CostToStart;

            var shortestPath = DijkstrasPath(endNodeCost);

            return (shortestPath, cost, found);
        }

        // TODO: compare paths to find shortest
        private static (List<Node> visited, int cost, bool found) FindPath(Node current, Node end, int edgeWeight, (List<Node> visited, int cost, bool found) path)
        {
            if (current.Name == end.Name)
            {
                // End node found.
                path.found = true;
                return path;
            }

            #region TODO: how compare paths?
            //int[] pathCosts = new int[current.Edges.Count];

            //for (int i = 0; i < current.Edges.Count; i++)
            //{
            //    if (path.cost > previousCost)
            //    {
            //        path.visited.Remove(current.Edges[i].Node);
            //        path.cost -= current.Edges[i].Weight;
            //    }
            //    pathCosts[i] = path.cost;
            //}
            #endregion

            foreach (var edge in current.Edges)
            {
                //var currentCost = path.cost + edge.Weight;

                // Don't visit a node twice.
                if (!path.visited.Contains(edge.Node))
                {
                    // Add node and cost to path...
                    path.visited.Add(edge.Node);
                    path.cost += edge.Weight;
                    // ...and move in to node.
                    return FindPath(edge.Node, end, edge.Weight, path);
                }

                // if current paths cost is <= ???
                //if (path.cost <= previousCost)
                //{
                //    return path;
                //}

                // TODO: dont remove`yet?
                //path.visited.Remove(edge.Node);
                //path.cost -= edge.Weight;
                //return path;
            }

            // Dead end, remove edge from path and return.
            //path.visited.RemoveAt(path.visited.Count - 1);
            path.visited.Remove(current);
            path.cost -= edgeWeight;
            return path;
        }

        /// <summary>
        /// Build and return
        /// </summary>
        /// <param name="end"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private static List<Node> DijkstrasPath(NodeCost end, List<Node> path = null)
        {
            if (path is null) path = new List<Node>();

            path.Add(end.Node);

            if (end.TowardStart == null) return path;

            return DijkstrasPath(end.TowardStart, path);
        }

        /// <summary>
        /// Implementation of Dijkstras algorithm, to find shortest paths from each node connected to <paramref name="start"/>.
        /// </summary>
        /// <param name="start">To find shortest paths to.</param>
        /// <param name="end">
        /// Node we are looking to find shortest path from <paramref name="start"/> to.
        /// Satisfies the search.
        /// </param>
        /// <returns>Min costs of <paramref name="nodes"/> when traveling to <paramref name="start"/>.</returns>
        private static List<NodeCost> DijkstrasCosts(Node start, Node end) // TODO remove end?
        {
            if (start is null || end is null) return null;

            List<NodeCost> nodeCosts = new List<NodeCost>();
            var startNodeCost = new NodeCost(start, null, 0);

            // Initialize the prio queue with start.
            List<NodeCost> prioQueue = new List<NodeCost>();
            prioQueue.Add(startNodeCost);

            var queuedCount = 0;

            // Calculate min cost from each node to start.
            while (prioQueue.Count > 0)
            {
                // Pop!
                var nodeCost = prioQueue.FirstOrDefault();
                prioQueue.RemoveAt(0);

                // For every edge of current node.
                foreach (var edge in nodeCost.Node.Edges)
                {
                    // Except edge back to node that has been visited.
                    if (nodeCosts.Find((nc) => nc.Node == nodeCost.Node) != null)
                    {
                        continue;
                    }

                    // Set node and cost towards start from edge node.
                    var edgeCostToStart = nodeCost.CostToStart + edge.Weight;
                    var edgeNodeCost = new NodeCost(edge.Node, nodeCost, edgeCostToStart);

                    var previousCost = nodeCosts.Find((nc) => nc.Node == edgeNodeCost.Node);

                    // Compare previous cost. 
                    if (previousCost != null)
                    {
                        if (previousCost.CostToStart > edgeNodeCost.CostToStart)
                        {
                            previousCost.CostToStart = edgeNodeCost.CostToStart; // TODO why no go here?
                            previousCost.TowardStart = edgeNodeCost.TowardStart;
                        }
                    }

                    // Queue edge to have its min cost to start determined.
                    prioQueue.Add(edgeNodeCost);
                    queuedCount++;
                }

                // Node is visited, min cost determined.
                if (nodeCosts.Find((nc) => nc.Node == nodeCost.Node) is null)
                {
                    nodeCosts.Add(nodeCost);
                }

                // Stop searching. 
                //if (nodeCost.Node == end) break; // TODO not neccesarily shortest if breaking...?
            }

            return nodeCosts;
        }

        public static int ShortestPath(Node start, Node visit, Node end)
        {
            throw new NotImplementedException();
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public static List<Path> Paths { get; set; } = new List<Path>();

        public static void PathHandler() // todo: Lägg till funktionalitet för att hantera om användaren vill ha ett delmål. Refactoring & optimisering?
        {                                // Lägg till rekursivitet
            if (InputOutput.UserNodes.Count == 2) // om användaren har valt 2 nodes
            {
                var startNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[0]); // Objekt som representerar start och slutnoderna som användaren anget.
                var endNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[1]);

                //if (!CheckForEndNodeFrom1NodeAway(startNode, endNode)) // Om slutnoden ligger brevid startnoden
                //    if (!CheckEndNodeFrom2NodesAway(startNode, endNode)) // Om slutnoden inte ligger brevid startnoden
                //        CheckEndNodeFrom3NodesAway(startNode, endNode); // Kollar alla noder som ligger 3 noder ifrån startnoden

                if (!CheckForEndNodeFrom1NodeAway(startNode, endNode))
                    PathFinder2(startNode, startNode, endNode);
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
                Paths.Add(new Path(totalWeight)); // Lägger till en ny Path() i List<Path> Paths
                return true; // Retunerar true så att algoritmen inte fortsätter att leta efter slutnod längre bort.
            }
            return false; // Retunerar false om metoden inte fann någon path till slutnoden så att algoritmen kan fortsätta leta efter slutnoden längre bort
        }

        /// <summary>
        /// Metod som söker efter slutnoden 2 noder bort från startnoden. Samt lägger till eventuellt funnen väg i List<Paths> Path
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
                        int totalWeight =
                            RouteCity.Edges.Find(f => f.Connections.Contains(nodeName) && f.Connections.Contains(nodeName2)).Weight +
                            RouteCity.Edges.Find(f => f.Connections.Contains(startNode.Name) && f.Connections.Contains(nodeName)).Weight;
                        visitedNodes.Add(nodeName); // Den mittersta nodens namn i pathen (första och sista finns i InputOutput.UserNodes

                        Paths.Add(new Path(totalWeight, visitedNodes));
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

                            Paths.Add(new Path(totalWeight, visitedNodes));
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

        public static List<string> VisitedNodes2 { get; set; } = new List<string>();
        public static int Count { get; set; } = -1;
        public static int TotalWeight2 { get; set; }
        public static void PathFinder2(Node startNode, Node currentNode, Node endNode) // rekursions version av algortimen.
        {
            foreach (var nodeName in currentNode.Connections)
                if (nodeName == endNode.Name)
                {
                    VisitedNodes2.Add(nodeName);

                    RouteCity.AddPath(startNode.Name, endNode.Name);
                    VisitedNodes2.Clear();
                }

            for (int i = 0; i < currentNode.Connections.Count; i++) // rekuserar Pathfinder med noder ett steg längre bort (vars connections rekurserars ovan)
            {
                if (i > 0)
                {
                    VisitedNodes2.RemoveAt(VisitedNodes2.Count - 1);
                }
                Node nextNode = RouteCity.Nodes.Find(f => f.Name == currentNode.Connections[i]);
                VisitedNodes2.Add(nextNode.Name);
                PathFinder2(startNode, nextNode, endNode);
            }
            Count++;
            PathFinder2(startNode, RouteCity.Nodes.Find(f => f.Name == currentNode.Connections[Count]), endNode);
        }
    }
}