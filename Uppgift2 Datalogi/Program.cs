namespace Uppgift2_Datalogi
{
    using Uppgift2_Datalogi.Views;

    class Program
    {
        static void Main()
        {
            Seeder.NodeSeeder();
            Seeder.EdgeSeeder();

            InputOutput.StartMenu();
            PathFinder.PathHandler();
            InputOutput.OutputResult();

            //var nodes = Seeder.Data();
            //var view = new PathFinderView(nodes);
            //view.StartMenu();
        }
    }
}