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

        public static int PathHandler()
        {
            if (InputOutput.UserNodes.Count == 2) // om användaren har valt 2 nodes
            {
                var startNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[0]);
                var endNode = RouteCity.Nodes.First(f => f.Name == InputOutput.UserNodes[1]);

                if (startNode.Connections.Contains(endNode.Name)) // Om slutnoden ligger brevid startnoden
                    return RouteCity.Edges.First(f => f.Name == startNode.Name + endNode.Name).Weight; // skickar tillbaka vikten av sträckan

                else  // Om slutnoden inte ligger brevid startnoden
                {
                    foreach (var nodeName in startNode.Connections) // Kollar alla noder som ligger brevid startnodens connections.
                        if (endNode == RouteCity.Nodes.First(f => f.Name == nodeName))
                        {

                        }
                }
            }
            else // om användaren har valt 2 nodes med en mellanlandning
            {

            }
            return 5;
        }
    }
}