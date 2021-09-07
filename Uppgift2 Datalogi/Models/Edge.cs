using System.Collections.Generic;

namespace Uppgift2_Datalogi
{
    class Edge
    {
        public List<string> Connections { get; set; } = new List<string>(); // Noderna som kanten ligger mellan.
        public int Weight; // sträckan på kanten

        public Edge(List<string> connections/*string name*/, int weight) // constuctor för kanter.
        {
            Connections = connections;
            Weight = weight;
        }
    }
}