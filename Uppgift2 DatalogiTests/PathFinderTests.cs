using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uppgift2_Datalogi;
using System.Linq;

namespace Uppgift2_Datalogi.Tests
{
    [TestClass()]
    public class PathFinderTests
    {
        private List<Node> Nodes;

        [TestInitialize()]
        public void TestInitialize()
        {
            Nodes = new List<Node>();
            Seeder.Seed(Nodes);
        }

        [TestMethod()]
        public void ShortestPathTest_WhenGoingFromAToB()
        {
            Node nodeA = Nodes.Find((node) => node.Name == "A");
            Node nodeB = Nodes.Find((node) => node.Name == "B");

            var expectedVisited = new List<Node>() { nodeA, nodeB };
            var expectedTotalCost = 4;
            var expectedIsPathFound = true;

            (List<Node> visited, int cost, bool found) expected = (expectedVisited, expectedTotalCost, expectedIsPathFound);

            var actual = PathFinder.ShortestPath(nodeA, nodeB);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ShortestPathTest_WhenGoingFromAToJ()
        {
            Node nodeA = Nodes.Find((node) => node.Name == "A");
            Node nodeB = Nodes.Find((node) => node.Name == "B");
            Node nodeC = Nodes.Find((node) => node.Name == "C");
            Node nodeG = Nodes.Find((node) => node.Name == "G");
            Node nodeJ = Nodes.Find((node) => node.Name == "J");

            var expectedVisited = new List<Node>() { nodeA, nodeB, nodeC, nodeG, nodeJ };
            var expectedTotalCost = 19;
            var expectedIsPathFound = true;

            (List<Node> visited, int cost, bool found) expected = (expectedVisited, expectedTotalCost, expectedIsPathFound);

            var actual = PathFinder.ShortestPath(nodeA, nodeJ);

            foreach (var node in actual.visited)
            {
                if (!expected.visited.Contains(node)) Assert.Fail();


            }

            //if (expected.visited.Except(actual.visited).ToList().Any() && actual.visited.Except(expected.visited).ToList().Any())
            //    Assert.Fail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ShortestPathTest_WhenGoingFromAToNodeNotInNetwork()
        {
            Node nodeA = Nodes.Find((node) => node.Name == "A");
            Node nodeB = Nodes.Find((node) => node.Name == "B");
            Node nodeC = Nodes.Find((node) => node.Name == "C");
            Node nodeG = Nodes.Find((node) => node.Name == "G");
            Node nodeOe = new Node("Ö");

            var actual = PathFinder.ShortestPath(nodeA, nodeOe);

            Assert.IsFalse(actual.found);
        }
    }
}