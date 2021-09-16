namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public static class PathFinder
    {
        /// <summary>
        /// Implementing Dijkstras algorithm the shortest path between <paramref name="start"/> and <paramref name="end"/> is found.
        /// </summary>
        /// <param name="start">Node to begin the path on, and in the same graph as <paramref name="end"/>.</param>
        /// <param name="end">Node to reach from <paramref name="start"/>.</param>
        /// <returns>Shortest path of nodes and total cost of that path.</returns>
        /// <time-complexity>O(n + m * e)</time-complexity>
        public static (List<Node> path, int cost) ShortestPath(Node start, Node end)
        {
            // Calculate min costs from each node to start.
            var minCostsToStart = DijkstrasCosts(start);

            var endNodeCost = minCostsToStart.Find((nc) => nc.Node == end);
            var cost = endNodeCost.CostToStart;

            // Build path from end to start.
            var shortestPath = DijkstrasPath(endNodeCost, new List<Node>());

            // Path is built from end to start so need to reverse it to get the right order.
            shortestPath.Reverse();

            return (shortestPath, cost);
        }

        /// <summary>
        /// Implementing Dijkstras algorithm the shortest path between <paramref name="start"/>, <paramref name="visit"/>, and <paramref name="end"/> is found.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="visit"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <time-complexity>O(2n + 2m * e)</time-complexity>
        public static (List<Node> path, int cost) ShortestPath(Node start, Node visit, Node end)
        {
            var (path, cost) = ShortestPath(start, visit);
            var visitToEnd = ShortestPath(visit, end);

            // Remove duplicate visit node.
            visitToEnd.path.RemoveAt(0);

            // Merge paths.
            path.AddRange(visitToEnd.path);

            return (path, cost + visitToEnd.cost);
        }

        /// <summary>
        /// Build the path in reverse, from end to start.
        /// </summary>
        /// <param name="end">Node to end the path on.</param>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <time-complexity>O(n)</time-complexity>
        private static List<Node> DijkstrasPath(NodeCost end, List<Node> path)
        {
            path.Add(end.Node);

            // Start is reached and the path is complete.
            if (end.TowardStart is null) return path;

            // Recursively build the path in reverse, from end until start, using the next node closest to start. 
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
        /// <returns>Min costs of each node when traveling to <paramref name="start"/>.</returns>
        /// <time-complexity>O(n * e)</time-complexity>
        private static List<NodeCost> DijkstrasCosts(Node start)
        {
            List<NodeCost> nodeCosts = new List<NodeCost>();
            var startNodeCost = new NodeCost(start, null, 0);

            // Initialize the queue with start.
            Queue<NodeCost> que = new Queue<NodeCost>();
            que.Enqueue(startNodeCost);

            var queuedCount = 0;

            // Calculate min cost from each node to start.
            while (que.Count > 0)
            {
                var currentNodeCost = que.Dequeue();

                // For every edge of current node.
                foreach (var edge in currentNodeCost.Node.Edges)
                {
                    // Except edge back to node that has been visited.
                    if (nodeCosts.Find((nc) => nc.Node == currentNodeCost.Node) != null) continue;

                    // Set node and cost towards start from edge node.
                    var edgeCostToStart = currentNodeCost.CostToStart + edge.Weight;
                    var edgeNodeCost = new NodeCost(edge.Node, currentNodeCost, edgeCostToStart);

                    var previousCost = nodeCosts.Find((nc) => nc.Node == edgeNodeCost.Node);

                    // Compare previous cost. 
                    if (previousCost != null)
                    {
                        if (previousCost.CostToStart > edgeNodeCost.CostToStart)
                        {
                            previousCost.CostToStart = edgeNodeCost.CostToStart;
                            previousCost.TowardStart = edgeNodeCost.TowardStart;
                        }
                    }

                    // Queue edge to have its min cost to start determined.
                    que.Enqueue(edgeNodeCost);
                    queuedCount++;
                }

                // Node is visited, min cost determined.
                if (nodeCosts.Find((nc) => nc.Node == currentNodeCost.Node) is null)
                {
                    nodeCosts.Add(currentNodeCost);
                }
            }

            return nodeCosts;
        }

        // TODO can we make recursive and for any amount of nodes to visit?
        public static int ShortestPath(Node start, Node end, Node[] visits)
        {
            throw new NotImplementedException();
        }
    }
}