namespace Uppgift2_Datalogi
{
    using Uppgift2_Datalogi.Views;

    internal static class Program
    {
        private static void Main()
        {
            Seeder.NodeSeeder();
            Seeder.EdgeSeeder();

            InputOutput.StartMenu();

            //var nodes = Seeder.Data();

            //var view = new PathFinderView(nodes);
            //view.Display();
        }
    }
}