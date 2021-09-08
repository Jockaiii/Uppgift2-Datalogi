namespace Uppgift2_Datalogi
{
    using System;
    using System.Collections.Generic;

    class InputOutput
    {
        public static List<string> UserNodes { get; set; } = new List<string>(); // public Lista som lagrar användarens val av noder i form av namn.

        /// <summary>
        /// Metod som agerar som en startmeny för användaren. Tar emot och tillkallar metoder för att kontrollera input. Och tillkallar eventuellt nödvändiga metoder.
        /// </summary>
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

        /// <summary>
        /// Metod som hanterar/tillkallar allt som har med noderna att göra. Från inläsning till kontrollering av godkänt input eller ej.
        /// </summary>
        /// <param name="userInput">Användarens input</param>
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

        /// <summary>
        /// Metod som ber om och läser in användarens input.
        /// </summary>
        /// <param name="currentNode">siffran på den nuvarande noden som användaren anger.</param>
        /// <returns>retunerar användarens input</returns>
        public static string InputNode(int currentNode)
        {
            Console.WriteLine($"Please choose node {currentNode + 1}");
            Console.Write("\nInput: ");
            return Console.ReadLine().ToUpper();
        }

        /// <summary>
        /// Metod som kontrollerar ifall användarens input är giltlig eller ej. samt meddelar användaren om utfallet.
        /// </summary>
        /// <param name="userNode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metod som kontrollerar att inputen användaren angivit är en existerande nod i busshållplats systemet.
        /// </summary>
        /// <param name="userNode">Den nuvarande bolen som användaren inputat</param>
        /// <returns>True eller false beronde på ifall noden existerar eller inte</returns>
        public static bool CheckValidNode(string userNode)
        {
            foreach (var node in RouteCity.Nodes)
                if (node.Name == userNode)
                    return true;
            return false;
        }

        /// <summary>
        /// Metod som lägger till godkänd input i List<string>UserNodes
        /// </summary>
        /// <param name="userNode">användarens godkända input</param>
        public static void AddUserNode(string userNode)
        {
            UserNodes.Add(userNode);
        }

        /// <summary>
        /// Metod som hanterar och skriver ut till användaren om den kortaste vägen som algoritmen hittade.
        /// </summary>
        public static void OutputResult()
        {
            if (PathFinder.Paths[0].VisitedNodes.Count > 0)
            {
                if (PathFinder.Paths[0].VisitedNodes.Count == 1)
                    Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.Paths[0].Weight}km with the path: {UserNodes[0]}-{PathFinder.Paths[0].VisitedNodes[0]}-{UserNodes[1]}");
                else if (PathFinder.Paths[0].VisitedNodes.Count == 2)
                    Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.Paths[0].Weight}km with the path: {UserNodes[0]}-{PathFinder.Paths[0].VisitedNodes[0]}-{PathFinder.Paths[0].VisitedNodes[1]}-{UserNodes[1]}");
                else if (PathFinder.Paths[0].VisitedNodes.Count == 3)
                    Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.Paths[0].Weight}km with the path: {UserNodes[0]}-{PathFinder.Paths[0].VisitedNodes[0]}-{PathFinder.Paths[0].VisitedNodes[1]}-{PathFinder.Paths[0].VisitedNodes[2]}-{UserNodes[1]}");
            }
            else
                Console.WriteLine($"The shortest route between {UserNodes[0]} and {UserNodes[1]} is {PathFinder.Paths[0].Weight}km");
        }
    }
}