using System;
using MenuSystem;
using MenuSystem.Enum;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // https://gitlab.cs.ttu.ee/rolaur/icd0008-2020f/-/blob/master/hw1-menu/MenuSystem/ConsoleApp/Program.cs



            Menu unary = new(MenuLevel.Level1); // negate, sqrt, square, abs value  
            unary.Add(new MenuItem("A", "Negate", () => "")); 
            unary.Add(new MenuItem("B", "Square root", () => "")); 
            unary.Add(new MenuItem("C", "Square", () => "")); 
            unary.Add(new MenuItem("D", "Absolute value", () => ""));
            
            Menu menu = new(MenuLevel.Level0);
            menu.Add(new MenuItem("A", "Binary", () => ""));
            menu.Add(new MenuItem("B", "Unary", unary.Run));
            menu.Run();
        }

        
        public static void MainMenu()
        {
            String userChoice;
            do
            {
                Console.Write("Choice: ");
                userChoice = Console.ReadLine()?.Trim().ToLower() ?? "";

            } while (userChoice == "");

            Console.WriteLine($"The choice was: {userChoice}.");
        }
    }
}