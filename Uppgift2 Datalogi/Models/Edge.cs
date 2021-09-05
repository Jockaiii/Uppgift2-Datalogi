namespace Uppgift2_Datalogi
{
    class Edge
    {
        public string Name; // namnet på kanten (Ex AB mellan nod A och B)
        public int Weight; // sträckan på kanten

        public Edge(string name, int weight) // constuctor för kanter.
        {
            Name = name;
            Weight = weight;
        }
    }
}