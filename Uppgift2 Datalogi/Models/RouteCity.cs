namespace Uppgift2_Datalogi
{
    using System.Collections.Generic;

    static class RouteCity
    {
        public static List<Node> Nodes { get; set; } = new List<Node>(); // Lista som innehåller alla noderna och deras egenskaper som finns i busshållplatssytemet.
        public static List<Edge> Edges { get; set; } = new List<Edge>(); // Lista som innehåller alla kanterna och deras egenskaper som finns i busshållplatssytemet
    }
}