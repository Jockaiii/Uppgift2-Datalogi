namespace Uppgift2_Datalogi.Views
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    internal static class ConsoleInput
    {
        /// <summary>
        /// Prompt user to select an item in the menu, if a valid menu item is not entered the user is prompted again.
        /// </summary>
        /// <returns>Selected menu items string representation.</returns>
        public static string PromptMenuSelect()
        {
            Console.WriteLine("Select");
            Console.WriteLine(" [1] Find shortest path between two nodes");
            Console.WriteLine(" [2] Find shortest path between three nodes");
            Console.WriteLine(" [3] Write network");
            Console.WriteLine(" [E] Exit");
            Console.Write("> ");
            var input = Console.ReadLine().ToUpper();
            Console.WriteLine();

            return input switch
            {
                "1" => input,
                "2" => input,
                "3" => input,
                "E" => input,
                _ => PromptMenuSelect(),
            };
        }

        /// <summary>
        /// Prompt user for a number, if a valid number is not entered the user is prompted again.
        /// </summary>
        /// <param name="minAllowed">Min allowed input.</param>
        /// <param name="maxAllowed">Max allowed input.</param>
        /// <returns>A number between <paramref name="minAllowed"/> and <paramref name="maxAllowed"/>.</returns>
        public static int PromptNumber(int minAllowed, int maxAllowed)
        {
            Console.WriteLine($"Enter a number between {minAllowed} and {maxAllowed} please.");
            Console.Write("> ");
            var input = Console.ReadLine();
            Console.WriteLine();

            try
            {
                var number = int.Parse(input, NumberStyles.Integer);
                return number > minAllowed && number < maxAllowed ? number : PromptNumber(minAllowed, maxAllowed);
            }
            catch (Exception)
            {
                return PromptNumber(minAllowed, maxAllowed);
            }
        }

        /// <summary>
        /// Prompts user to select a node.
        /// </summary>
        /// <param name="validNodes">Nodes to select from.</param>
        /// <param name="nodePurpose">Purpose of the node to instruct user, i.e. visiting.</param>
        /// <returns>User selected node.</returns>
        public static Node PromptNode(List<Node> validNodes, string nodePurpose)
        {
            Console.WriteLine($"Select {nodePurpose} node");

            var validInput = " [";
            foreach (var validNode in validNodes)
            {
                validInput += $"{validNode.Name}|";
            }
            validInput = validInput.TrimEnd('|');
            validInput += "]";
            Console.WriteLine(validInput);

            Console.Write("> ");
            var input = Console.ReadLine();
            Console.WriteLine();

            var node = validNodes.Find((node) => string.Equals(node.Name, input, StringComparison.OrdinalIgnoreCase));

            return node ?? PromptNode(validNodes, nodePurpose);
        }
    }
}
