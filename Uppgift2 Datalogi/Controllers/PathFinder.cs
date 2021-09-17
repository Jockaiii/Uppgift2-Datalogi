namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public static class PathFinder
    {
        /// <summary>
        /// Number of node costs queued for evalauation when calculating shortest path from any node to a starting node.
        /// </summary>
        public static int DijkstraQueuedNodeCosts { get; private set; }

        /// <summary>
        /// Implementing Dijkstras algorithm to find the shortest path between <paramref name="start"/> and <paramref name="end"/>.
        /// </summary>
        /// <param name="start">Node to begin the path on, and in the same graph as <paramref name="end"/>.</param>
        /// <param name="end">Node to reach from <paramref name="start"/>.</param>
        /// <returns>Shortest path of nodes and total cost of that path.</returns>
        /// <time-complexity-worst-case>O(n + m * e)</time-complexity-worst-case>
        public static (List<Node> path, int cost) ShortestPath(Node start, Node end)
        {
            // Calculate min costs from each node to start.
            DijkstraQueuedNodeCosts = 0;
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
        /// Implementing Dijkstras algorithm to find the shortest path between <paramref name="start"/>, <paramref name="visit"/>, and <paramref name="end"/>.
        /// </summary>
        /// <param name="start">Node to begin the path on, and in the same graph as <paramref name="end"/>.</param>
        /// <param name="visit">Node to visit between <paramref name="start"/> and <paramref name="end"/>.</param>
        /// <param name="end">Node to reach from <paramref name="start"/>.</param>
        /// <returns>Shortest path of nodes and total cost of that path.</returns>
        /// <time-complexity-worst-case>O(2n + 2m * e)</time-complexity-worst-case>
        public static (List<Node> path, int cost) ShortestPath(Node start, Node visit, Node end)
        {
            DijkstraQueuedNodeCosts = 0;
            var (path, cost) = ShortestPath(start, visit);
            var visitToEnd = ShortestPath(visit, end);

            // Remove duplicate visit node.
            visitToEnd.path.RemoveAt(0);

            // Merge paths.
            path.AddRange(visitToEnd.path);

            return (path, cost + visitToEnd.cost);
        }

        /// <summary>
        /// Build the <paramref name="path"/> in reverse, from <paramref name="end"/> to start.
        /// </summary>
        /// <param name="end">Node to end the path on.</param>
        /// <param name="path">Concatenate the path onto this.</param>
        /// <returns>List of nodes representing the path, but in reverse order.</returns>
        /// <time-complexity-worst-case>O(n) iterates through the linked list of nodes.</time-complexity-worst-case>
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
        /// <returns>Min costs of each node when traveling to <paramref name="start"/>.</returns>
        /// <time-complexity-worst-case>O(n * e) iterates through all nodes and all edges connected to node.</time-complexity-worst-case>
        private static List<NodeCost> DijkstrasCosts(Node start)
        {
            List<NodeCost> nodeCosts = new List<NodeCost>();
            var startNodeCost = new NodeCost(start, null, 0);

            // Initialize the queue with start.
            Queue<NodeCost> que = new Queue<NodeCost>();
            que.Enqueue(startNodeCost);

            // Calculate min cost from each node to start.
            while (que.Count > 0)
            {
                var currNodeCost = que.Dequeue();

                // For every edge of current node.
                foreach (var edge in currNodeCost.Node.Edges)
                {
                    // Except edge back to node that has been determined.
                    if (nodeCosts.Find((nc) => nc.Node == edge.Node) != null) continue;

                    // Set node and cost towards start from edge node.
                    var edgeCostToStart = currNodeCost.CostToStart + edge.Weight;
                    var edgeNodeCost = new NodeCost(edge.Node, currNodeCost, edgeCostToStart);

                    var prevEdgeCost = nodeCosts.Find((nc) => nc.Node == edgeNodeCost.Node);

                    if (prevEdgeCost != null)
                    {
                        // Compare previous node cost for edge and redirect current node cost if closer. 
                        if (prevEdgeCost.CostToStart < edgeCostToStart)
                        {
                            currNodeCost.CostToStart = prevEdgeCost.CostToStart + edge.Weight;
                            currNodeCost.TowardStart = prevEdgeCost;
                        }
                    }

                    // Queue edge to have its min cost to start determined.
                    que.Enqueue(edgeNodeCost);
                    DijkstraQueuedNodeCosts++;
                }

                var previousCostIndex = nodeCosts.FindIndex((nc) => nc.Node == currNodeCost.Node);

                // Node cost with previous nodes is determined, add, or update if closer.
                if (previousCostIndex < 0)
                {
                    nodeCosts.Add(currNodeCost);
                }
                else if (nodeCosts[previousCostIndex].CostToStart > currNodeCost.CostToStart)
                {
                    nodeCosts[previousCostIndex] = currNodeCost;
                }
            }

            return nodeCosts;
        }
    }
}