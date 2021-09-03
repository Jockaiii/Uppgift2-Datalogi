namespace Uppgift2_Datalogi
{
    class Program
    {
        static void Main()
        {
            Seeder.NodeSeeder();
            Seeder.EdgeSeeder();

            InputOutput.StartMenu();
        }
    }
}