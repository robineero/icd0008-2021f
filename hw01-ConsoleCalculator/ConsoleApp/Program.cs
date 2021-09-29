using System;
using System.Threading;
using MenuSystem;
using MenuSystem.Enum;

namespace ConsoleApp
{
    class Program
    {

        private static double CalculatorCurrentValue = 0.0;
        static void Main(string[] args)
        {
            // https://gitlab.cs.ttu.ee/rolaur/icd0008-2020f/-/blob/master/hw1-menu/MenuSystem/ConsoleApp/Program.cs
            
            Menu unary = new(MenuLevel.Level1, "---- Level 1 - choose unary operation"); // negate, sqrt, square, abs value  
            unary.Add(new MenuItem("A", "Negate", () => "")); 
            unary.Add(new MenuItem("B", "Square root", () => "")); 
            unary.Add(new MenuItem("C", "Square", () => "")); 
            unary.Add(new MenuItem("D", "Absolute value", () => ""));   
            
            Menu binary = new(MenuLevel.Level1, "---- Level 1 - choose binary operation"); // +, -, /, *, x power y
            binary.Add(new MenuItem("+", "Plus", Add)); 
            binary.Add(new MenuItem("-", "Minus", () => "")); 
            binary.Add(new MenuItem("/", "Divide", () => "")); 
            binary.Add(new MenuItem("*", "Multiply", () => ""));
            binary.Add(new MenuItem("P", "Power", () => ""));
            
            Menu menu = new(MenuLevel.Level0, "---- Level 0 - choose type");
            menu.Add(new MenuItem("A", "Binary", binary.Run));
            menu.Add(new MenuItem("B", "Unary", unary.Run));
            menu.Run();
        }
        
        public static string Add()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} + ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            CalculatorCurrentValue = CalculatorCurrentValue + converted;
            Console.Write($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }
    }
}