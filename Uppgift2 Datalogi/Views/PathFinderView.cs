namespace Uppgift2_Datalogi.Views
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// View for displaying a network of nodes and finding the shortest path between 2 or 3 nodes.
    /// </summary>
    public class PathFinderView
    {
        private List<Node> Nodes { get; }
        private Node Start { get; set; }
        private Node Visit { get; set; }
        private Node End { get; set; }

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
            Start = ConsoleInput.PromptNode(allowedNodes, "start");

            allowedNodes.Remove(Start);
            End = ConsoleInput.PromptNode(allowedNodes, "end");

            var (path, cost) = PathFinder.ShortestPath(Start, End);

            Console.WriteLine($"Shortest path found between {Start.Name} and {End.Name}:\n");
            WritePath(path);
            Console.WriteLine($"Total cost: {cost}\n");
        }

        /// <summary>
        /// Find the shortest path between three nodes.
        /// </summary>
        private void FindShortestPathThreeNodes()
        {
            var allowedNodes = new List<Node>(Nodes);
            Start = ConsoleInput.PromptNode(allowedNodes, "start");

            allowedNodes.Remove(Start);
            End = ConsoleInput.PromptNode(allowedNodes, "end");

            allowedNodes.Remove(End);
            Visit = ConsoleInput.PromptNode(allowedNodes, "visiting");

            var (path, cost) = PathFinder.ShortestPath(Start, Visit, End);

            Console.WriteLine($"Shortest path found between {Start.Name} and {End.Name} when also visiting {Visit.Name}:\n");
            WritePath(path);
            Console.WriteLine($"Total cost: {cost}\n");
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
