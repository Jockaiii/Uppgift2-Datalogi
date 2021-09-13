namespace Uppgift2_Datalogi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NodeCost
    {
        public Node Node { get; set; }
        public NodeCost TowardStart { get; set; }
        public int CostToStart { get; set; }

        public NodeCost(Node node, NodeCost towardStart, int costToStart)
        {
            Node = node;
            TowardStart = towardStart;
            CostToStart = costToStart;
        }

        public override string ToString()
        {
            return $"{Node.Name}, Start -> {TowardStart.Node.Name}, Cost: {CostToStart}";
        }
    }
}
