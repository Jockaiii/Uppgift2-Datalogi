using System;
using System.Collections.Generic;
using System.Text;

namespace Uppgift2_Datalogi
{
    class Node
    {
        public string Name { get; set; }
        public List<string> Connections { get; set; }

        public Node(string name, List<string> connections)
        {
            Name = name;
            Connections = connections;
        }
    }
}
