namespace Uppgift2_Datalogi.Views
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PathFinderView
    {
        private List<Node> Nodes { get; set; }
        private Node Start { get; set; }
        private Node Visit { get; set; }
        private Node End { get; set; }

        public PathFinderView(List<Node> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Start console user interface.
        /// </summary>
        public void StartMenu()
        {
            var exit = false;
            while (!exit)
            {
                // Display menu.
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

        private void FindShortestPathTwoNodes()
        {
            var allowedNodes = new List<Node>(Nodes);
            Start = ConsoleInput.PromptNode(allowedNodes, "start");

            allowedNodes.Remove(Start);
            End = ConsoleInput.PromptNode(allowedNodes, "end");

            var (visited, cost, found) = PathFinder.ShortestPath(Start, End);

            if (found)
            {
                Console.WriteLine($"Shortest path found between {Start.Name} and {End.Name}:\n");
                WritePath(visited);
                Console.WriteLine($"Total cost: {cost}\n");
            }
            else
            {
                Console.WriteLine($"No path was found between {Start.Name} and {End.Name}.\n");
            }
        }

        private void FindShortestPathThreeNodes()
        {
            var allowedNodes = new List<Node>(Nodes);
            Start = ConsoleInput.PromptNode(allowedNodes, "start");

            allowedNodes.Remove(Start);
            End = ConsoleInput.PromptNode(allowedNodes, "end");

            allowedNodes.Remove(End);
            Visit = ConsoleInput.PromptNode(allowedNodes, "visiting");

            // TODO
        }

        private void WriteNetwork()
        {
            Console.WriteLine("<Node>:\t<Edge(Weight)> ...\n");
            foreach (var node in Nodes)
            {
                Console.WriteLine(node);
            }
            Console.WriteLine();
        }

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
