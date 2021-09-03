using System;
using System.Collections.Generic;
using Uppgift2_Datalogi.Views;

namespace Uppgift2_Datalogi
{
    class Program
    {
        static void Main()
        {
            Seeder.NodeSeeder();
            Seeder.EdgeSeeder();

            InputOutput.UserChoice();

            var nodes = new List<Node>();
            Seeder.Seed(nodes);

            var view = new PathFinderView(nodes);
            view.StartMenu();
        }
    }
}