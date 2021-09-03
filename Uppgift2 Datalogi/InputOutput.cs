namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;

    class InputOutput
    {
        public static List<string> UserNodes { get; set; } = new List<string>();
        public static bool OnlyOneStop { get; set; }

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
                        OnlyOneStop = true;
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

                //if (OnlyOneStop) // om användaren har valt 2 punkter med endast ett delmål
                //    CheckOnlyOneStop();
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

        //public static void CheckOnlyOneStop()
        //{
        //    var node1 = RouteCity.Nodes.Where(w => w.Name == UserNodes[0]);
        //    var node2 = RouteCity.Nodes.Where(w => w.Name == UserNodes[1]);

        //    foreach (var property1 in node1)
        //        foreach (var property2 in node2)
        //            foreach (var element1 in property1.Connections)
        //                foreach (var element2 in property2.Connections)
        //                {
        //                    if (element1 == element2)
        //                    {
                                
        //                    }
        //                }
        //    Console.WriteLine("Those nodes can not be reached with just one stop");
        //    UserChoice();
        //}

        public static bool CheckValidNode(string userNode)
        {
            foreach (var node in RouteCity.Nodes)
                if (node.Name == userNode)
                    return true;
            return false;
        }
    }
}