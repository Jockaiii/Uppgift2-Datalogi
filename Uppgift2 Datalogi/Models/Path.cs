using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift2_Datalogi.Models
{
    public class Path
    {
        public int Weight { get; set; }
        public List<string> VisitedNodes { get; set; } = new List<string>();

        public Path(int weight)
        {
            Weight = weight;
        }
        
        public Path(int weight, List<string> visitedNodes)
        {
            Weight = weight;
            VisitedNodes = visitedNodes;
        }
    }
}
