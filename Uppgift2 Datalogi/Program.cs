using System;
using System.Collections.Generic;

namespace Uppgift2_Datalogi
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = new List<Node>();
            Seeder.Seed(nodes);

            foreach (var node in nodes)
            {
                Console.WriteLine(node);
            }
        }
    }
}