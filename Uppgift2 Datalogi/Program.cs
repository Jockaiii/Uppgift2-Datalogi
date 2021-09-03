
namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;
    using Uppgift2_Datalogi.Views;

    class Program
    {
        static void Main()
        {
            Seeder.NodeSeeder();
            Seeder.EdgeSeeder();

            InputOutput.StartMenu();

            var nodes = new List<Node>();
            Seeder.Seed(nodes);

            var view = new PathFinderView(nodes);
            view.StartMenu();
        }
    }
}