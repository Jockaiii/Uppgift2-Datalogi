namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;

    class InputOutput
    {
        public static List<string> UserNodes { get; set; } = new List<string>();

        public static void UserChoice()
        {
            string userInput;
            do
            {
                Console.WriteLine("[1] To calculate the shortest route between 2 nodes of your choice\n" +
                    "[2] To calculate the shortest route between 3 nodes of your choice \n[3] To close the application\n");
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
                } while (!CheckNodeInput(currentNode, userNode));
                AddUserNode(userNode);
            }
        }
        public static string InputNode(int currentNode)
        {
            Console.WriteLine($"Please choose node {currentNode + 1}");
            Console.Write("\nInput: ");
            return Console.ReadLine().ToUpper();
        }

        public static bool CheckNodeInput(int currentNode, string userNode)
        {
            List<string> nodes = new List<string>
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"
            };

            if (nodes.Contains(userNode) && !UserNodes.Contains(userNode))
                return true;
            else
            {
                Console.Clear();
                if (!nodes.Contains(userNode))
                    Console.WriteLine("That node does not exist, please try again");
                else if (userNode.Contains(userNode))
                    Console.WriteLine("That node has already been selected, please choose another one.");
                else
                    Console.WriteLine("Wrong type of input, please try again\n");
                return false;
            }
        }

        public static void AddUserNode(string userNode)
        {
            UserNodes.Add(userNode);
        }
    }
}