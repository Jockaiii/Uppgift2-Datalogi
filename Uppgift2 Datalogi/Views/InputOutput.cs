namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;

    class InputOutput
    {
        public static List<string> UserNodes { get; set; } = new List<string>();

        public static bool ReverseResultOutput { get; set; }

        public static void StartMenu()
        {
            string userInput;
            do
            {
                Console.WriteLine("[1] To calculate the shortest route between 2 nodes of your choice\n" +
                    "[2] To calculate the shortest route between 2 nodes of your choice with a stop of your choice\n[3] To close the application\n");
                Console.Write("Input: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        NodeInputHandler(int.Parse(userInput));
                        break;
                    case "2":
                        NodeInputHandler(int.Parse(userInput));
                        break;
                    case "3":
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Wrong type of input, please try again\n");
                        break;
                }
            } while (!int.TryParse(userInput, out _) && userInput.Length > 0 && userInput.Length < 4);
        }
        public static void NodeInputHandler(int userInput)
        {
            string userNode;
            for (int currentNode = 0; currentNode < userInput + 1; currentNode++)
            {
                Console.Clear();
                do
                {
                    userNode = InputNode(currentNode);
                } while (!CheckNodeInput(userNode));

                AddUserNode(userNode);
            }
        }

        public static string InputNode(int currentNode)
        {
            Console.WriteLine($"Please choose node {currentNode + 1}");
            Console.Write("\nInput: ");
            return Console.ReadLine().ToUpper();
        }

        public static bool CheckNodeInput(string userNode)
        {
            if (CheckValidNode(userNode) && !UserNodes.Contains(userNode))
                return true;
            else
            {
                Console.Clear();
                if (!CheckValidNode(userNode))
                    Console.WriteLine("That node does not exist, please try again\n");
                else if (userNode.Contains(userNode))
                    Console.WriteLine("That node has already been selected, please choose another one\n");
                else
                    Console.WriteLine("Wrong type of input, please try again\n");
                return false;
            }
        }

        public static void AddUserNode(string userNode)
        {
            UserNodes.Add(userNode);
        }

        public static bool CheckValidNode(string userNode)
        {
            foreach (var node in RouteCity.Nodes)
                if (node.Name == userNode)
                    return true;
            return false;
        }

        public static void OutputResult()
        {
            if (!ReverseResultOutput) // Om algoritmen hittade en väg utan att sortera UserNodes
            {
                if (PathFinder.VisitedNodes.Count > 0)
                {
                    if (PathFinder.VisitedNodes.Count == 1)
                        Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.TotalWeight}km with the path: {UserNodes[0]}-{PathFinder.VisitedNodes[0]}-{UserNodes[1]}");
                    else
                        Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.TotalWeight}km with the path: {UserNodes[0]}-{PathFinder.VisitedNodes[0]}-{PathFinder.VisitedNodes[1]}-{UserNodes[1]}");
                }
                else
                    Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.TotalWeight}km");
            }
            else
            {
                if (PathFinder.VisitedNodes.Count > 0)
                {
                    if (PathFinder.VisitedNodes.Count == 1)
                        Console.WriteLine($"The shortest route between {UserNodes[1]} and {UserNodes[0]} is {PathFinder.TotalWeight}km with the path: {UserNodes[1]}-{PathFinder.VisitedNodes[0]}-{UserNodes[0]}");
                    else if (PathFinder.VisitedNodes.Count == 2)
                        Console.WriteLine($"The shortest route between {UserNodes[1]} and {UserNodes[0]} is {PathFinder.TotalWeight}km with the path: {UserNodes[1]}-{PathFinder.VisitedNodes[1]}-{PathFinder.VisitedNodes[0]}-{UserNodes[0]}");
                }
                else
                    Console.WriteLine($"The shortest route between {UserNodes[1]} and {UserNodes[0]} is {PathFinder.TotalWeight}km");
            }
            
        }
    }
}