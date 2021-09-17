namespace Uppgift2_Datalogi.Models
{
    /// <summary>
    /// Stores data related to a node used when using an implementation of Dijkstras algorithm.
    /// </summary>
    public class NodeCost
    {
        public Node Node { get; set; }

        /// <summary>
        /// Node to traverse for minimum cost reaching start.
        /// </summary>
        public NodeCost TowardStart { get; set; }

        /// <summary>
        /// Cost for reaching start from this node.
        /// </summary>
        public int CostToStart { get; set; }

        public NodeCost(Node node, NodeCost towardStart, int costToStart)
        {
            Node = node;
            TowardStart = towardStart;
            CostToStart = costToStart;
        }

        /// <summary>
        /// String description of the node cost.
        /// </summary>
        /// <returns>Name of node, name of node that leads towards start, and cost for reaching start from this node.</returns>
        public override string ToString()
        {
            return $"{Node.Name}, Start -> {TowardStart.Node.Name}, Cost: {CostToStart}";
        }
    }
}
