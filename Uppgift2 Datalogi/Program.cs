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
        }
    }
}