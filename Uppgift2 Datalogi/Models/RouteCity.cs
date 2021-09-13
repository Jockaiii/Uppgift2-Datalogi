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
            for (int i = 0; i + 1 < PathFinder.VisitedNodes2.Count - 1; i++) // Lägger till vikten av alla mellan kanterna vid mellan noderna.
            {
                weight += Edges.Find(f => f.Connections.Contains(PathFinder.VisitedNodes2[i]) && f.Connections.Contains(PathFinder.VisitedNodes2[i + 1])).Weight;
            }
            weight += Edges.Find(f => f.Connections.Contains(startNodeName) && f.Connections.Contains(PathFinder.VisitedNodes2[0])).Weight + Edges.Find(f => f.Connections.Contains(endNodeName) && f.Connections.Contains(PathFinder.VisitedNodes2[^1])).Weight; // Lägger till vikten av kanterna brevid start och slutnoden.
            PathFinder.Paths.Add(new Models.Path(weight, PathFinder.VisitedNodes2));
        }
    }
}