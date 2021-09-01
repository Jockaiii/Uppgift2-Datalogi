using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift2_Datalogi
{
    class Node
    {
        public List<(Node node, int weight)> Edges = new List<(Node node, int weight)>();
    }
}
