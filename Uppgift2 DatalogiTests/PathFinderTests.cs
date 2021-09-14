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
            // Setup the node network with seeder data.
            Nodes = Seeder.Data();
        }

        [TestMethod()]
        public void ShortestPathTest_WhenGoingFromAToB()
        {
            Node nodeA = Nodes.Find((node) => node.Name == "A");
            Node nodeB = Nodes.Find((node) => node.Name == "B");

            var expectedVisited = new List<Node>() { nodeA, nodeB };
            var expectedTotalCost = 4;

            (List<Node> path, int cost) expected = (expectedVisited, expectedTotalCost);

            var actual = PathFinder.ShortestPath(nodeA, nodeB);

            foreach (var node in actual.path)
            {
                if (!expected.path.Contains(node)) Assert.Fail("actual node is not in expected");
            }
            foreach (var node in expected.path)
            {
                if (!actual.path.Contains(node)) Assert.Fail("expected node is not in actual");
            }
        }

        [TestMethod()]
        public void ShortestPathTest_WhenGoingFromAToJ()
        {
            Node nodeA = Nodes.Find((node) => node.Name == "A");
            Node nodeB = Nodes.Find((node) => node.Name == "B");
            Node nodeH = Nodes.Find((node) => node.Name == "H");
            Node nodeJ = Nodes.Find((node) => node.Name == "J");

            var expectedVisited = new List<Node>() { nodeA, nodeB, nodeH, nodeJ };
            var expectedTotalCost = 18;

            (List<Node> path, int cost) expected = (expectedVisited, expectedTotalCost);

            var actual = PathFinder.ShortestPath(nodeA, nodeJ);

            // Assert fail if any node visited was not in expected
            foreach (var node in actual.path)
            {
                if (!expected.path.Contains(node)) Assert.Fail();
            }
            // Assert fail if any node in expected was not actually visited.
            foreach (var node in expected.path)
            {
                if (!actual.path.Contains(node)) Assert.Fail();
            }

            // Assert actual cost to be as expected.
            Assert.AreEqual(expectedTotalCost, actual.cost);
        }
    }
}