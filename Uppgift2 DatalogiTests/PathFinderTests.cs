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
            // Setup the node network with seed data.
            Nodes = Seeder.Data();
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

            // Assert fail if any node visited was not in expected
            foreach (var node in actual.visited)
            {
                if (!expected.visited.Contains(node)) Assert.Fail();
            }
            // Assert fail if any node in expected was not actually visited.
            foreach (var node in expected.visited)
            {
                if (!actual.visited.Contains(node)) Assert.Fail();
            }

            // Assert actual cost to be as expected.
            Assert.AreEqual(expectedTotalCost, actual.cost);

            // Assert that a path was found between start and end node. 
            Assert.IsTrue(actual.found);
        }

        [TestMethod()]
        public void ShortestPathTest_WhenGoingFromAToNodeNotInNetwork()
        {
            Node nodeA = Nodes.Find((node) => node.Name == "A");
            Node nodeB = Nodes.Find((node) => node.Name == "B");
            Node nodeC = Nodes.Find((node) => node.Name == "C");
            Node nodeG = Nodes.Find((node) => node.Name == "G");
            Node nodeZ = new Node("Z");

            var actual = PathFinder.ShortestPath(nodeA, nodeZ);

            Assert.IsFalse(actual.found);
        }
    }
}