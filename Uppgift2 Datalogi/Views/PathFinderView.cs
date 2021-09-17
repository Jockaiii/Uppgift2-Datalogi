namespace Uppgift2_Datalogi.Views
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// View for displaying a network of nodes and finding the shortest path between 2 or 3 nodes.
    /// </summary>
    public class PathFinderView
    {
        private List<Node> Nodes { get; }

        /// <summary>
        /// Creates a view for a network of <paramref name="nodes"/>.
        /// </summary>
        /// <param name="nodes">All nodes of the network.</param>
        public PathFinderView(List<Node> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Start the view.
        /// </summary>
        public void Display()
        {
            // Present menu until exit.
            var exit = false;
            while (!exit)
            {
                switch (ConsoleInput.PromptMenuSelect())
                {
                    case "1":
                        FindShortestPathTwoNodes();
                        break;
                    case "2":
                        FindShortestPathThreeNodes();
                        break;
                    case "3":
                        WriteNetwork();
                        break;
                    case "E":
                        exit = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Find the shortest path between two nodes.
        /// </summary>
        private void FindShortestPathTwoNodes()
        {
            var allowedNodes = new List<Node>(Nodes);

            var start = ConsoleInput.PromptNode(allowedNodes, "start");
            allowedNodes.Remove(start);

            var end = ConsoleInput.PromptNode(allowedNodes, "end");

            PathFinder.SearchCost = 0;
            var (path, cost) = PathFinder.ShortestPath(start, end);

            Console.WriteLine($"Shortest path found between {start.Name} and {end.Name}:\n");
            WritePath(path);
            Console.WriteLine($"Total cost of path: {cost}\n");
            Console.WriteLine($"Search cost: {PathFinder.SearchCost}\n");
        }

        /// <summary>
        /// Find the shortest path between three nodes.
        /// </summary>
        private void FindShortestPathThreeNodes()
        {
            var allowedNodes = new List<Node>(Nodes);

            var start = ConsoleInput.PromptNode(allowedNodes, "start");
            allowedNodes.Remove(start);

            var visit = ConsoleInput.PromptNode(allowedNodes, "visiting");
            allowedNodes.Remove(visit);

            var end = ConsoleInput.PromptNode(allowedNodes, "end");

            PathFinder.SearchCost = 0;
            var (path, cost) = PathFinder.ShortestPath(start, visit, end);

            Console.WriteLine($"Shortest path found between {start.Name} and {end.Name} when also visiting {visit.Name}:\n");
            WritePath(path);
            Console.WriteLine($"Total cost of path: {cost}\n");
            Console.WriteLine($"Search cost: {PathFinder.SearchCost}\n");
        }

        /// <summary>
        /// Write nodes in the network to console.
        /// </summary>
        private void WriteNetwork()
        {
            Console.WriteLine("<Node>:\t<Edge(Weight)> ...\n");
            foreach (var node in Nodes)
            {
                Console.WriteLine(node);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Write path to console.
        /// </summary>
        /// <param name="path">Path of nodes, in order, starting at first element.</param>
        private void WritePath(List<Node> path)
        {
            var pathStr = string.Empty;

            foreach (var node in path)
            {
                pathStr += $"{node.Name} -> ";
            }

            Console.WriteLine(pathStr[0..^4] + "\n");
        }
    }
}