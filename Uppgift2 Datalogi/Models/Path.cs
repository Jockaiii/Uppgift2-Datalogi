namespace Uppgift2_Datalogi.Models
{
    using System.Collections.Generic;
    public class Path
    {
        public int Weight { get; set; } // Den totala sträckan för vägen.
        public List<string> VisitedNodes { get; set; } = new List<string>(); // De noderna som vägen passerar.

        public Path(int weight) // constuctor för slutnoder som ligger previd startnoden.
        {
            Weight = weight;
        }

        public Path(int weight, List<string> visitedNodes) // constuctor för övrig placering av slutnod.
        {
            Weight = weight;
            VisitedNodes = visitedNodes;
        }
    }
}