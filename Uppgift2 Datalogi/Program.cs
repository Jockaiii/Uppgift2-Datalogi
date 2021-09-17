namespace Uppgift2_Datalogi
{
    using Uppgift2_Datalogi.Views;

    internal static class Program
    {
        private static void Main()
        {
            var nodes = Seeder.Data();
            var view = new PathFinderView(nodes);
            view.Display();

            // remove
            //var nodes = Seeder.Data();
            //var nodeA = nodes.Find((n) => n.Name == "A");
            //var nodeJ = nodes.Find((n) => n.Name == "J");
            //PathFinder.ShortestPath(nodeJ, nodeA);
        }
    }
}