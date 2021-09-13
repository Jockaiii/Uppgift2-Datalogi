namespace Uppgift2_Datalogi
{
    using Uppgift2_Datalogi.Views;

    internal static class Program
    {
        private static void Main()
        {
            //Seeder.NodeSeeder();
            //Seeder.EdgeSeeder();

            //InputOutput.StartMenu();
            //PathFinder.PathHandler();
            //InputOutput.OutputResult();

            var nodes = Seeder.Data();
            var start = nodes.Find(n => n.Name == "A");
            var   end = nodes.Find(n => n.Name == "J");

            //var dijkstras = PathFinder.DijkstrasCosts(start, end);

            var shortestPath = PathFinder.DijkstrasShortestPath(start, end);

            // TODO: can find path, but not shortest
            //var view = new PathFinderView(nodes);
            //view.StartMenu();
        }
    }
}