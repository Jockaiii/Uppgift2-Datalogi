namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    public static class PathFinder
    {
        public static (List<Node> visited, int cost, bool found) ShortestPath(Node start, Node end)
        {
            var paths = new List<(List<Node> visited, int cost, bool found)>();

            // TODO: get all possible paths.

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
    }
}
