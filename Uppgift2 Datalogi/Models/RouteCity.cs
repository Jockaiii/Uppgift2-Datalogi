namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    static class RouteCity
    {
        public static List<Node> Nodes { get; set; } = new List<Node>(); // Lista som innehåller alla noderna och deras egenskaper som finns i busshållplatssytemet.
        public static List<Edge> Edges { get; set; } = new List<Edge>(); // Lista som innehåller alla kanterna och deras egenskaper som finns i busshållplatssytemet

        public static void AddPath(string startNodeName, string endNodeName)
        {
            int weight = 0;
            for (int i = 0; i + 1 < PathFinder.VisitedNodes.Count - 1; i++) // Lägger till vikten av alla kanterna brevid alla noder förutom start och slutnoderna.
                weight += Edges.Find(f => f.Connections.Contains(PathFinder.VisitedNodes[i]) && f.Connections.Contains(PathFinder.VisitedNodes[i + 1])).Weight;

            if (PathFinder.VisitedNodes.Count > 0) // Om det finns en nod mellan start och slutnoden
                weight += Edges.Find(f => f.Connections.Contains(startNodeName) && f.Connections.Contains(PathFinder.VisitedNodes[0])).Weight + Edges.Find(f => f.Connections.Contains(PathFinder.VisitedNodes[^1]) && f.Connections.Contains(endNodeName)).Weight; // Lägger till vikten av kanterna brevid start och slutnoden.
            else // om det bara är start och slutnoden i pathen.
                weight += Edges.Find(f => f.Connections.Contains(InputOutput.UserNodes[0]) && f.Connections.Contains(InputOutput.UserNodes[1])).Weight;

            PathFinder.Paths.Add(new Models.Path(weight, PathFinder.VisitedNodes));
        }
    }
}