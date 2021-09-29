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
            unary.Add(new MenuItem("A", "Negate", Negate)); 
            unary.Add(new MenuItem("B", "Square root", SquareRoot)); 
            unary.Add(new MenuItem("C", "Square", Square)); 
            unary.Add(new MenuItem("D", "Absolute value", Absolute));   
            
            Menu binary = new(MenuLevel.Level1, "---- Level 1 - choose binary operation"); // +, -, /, *, x power y
            binary.Add(new MenuItem("+", "Plus", Plus)); 
            binary.Add(new MenuItem("-", "Minus", Minus)); 
            binary.Add(new MenuItem("/", "Divide", Division)); 
            binary.Add(new MenuItem("*", "Multiply", Multiply));
            binary.Add(new MenuItem("P", "Power", Power));
            
            Menu menu = new(MenuLevel.Level0, "---- Level 0 - choose type");
            menu.Add(new MenuItem("A", "Binary", binary.Run));
            menu.Add(new MenuItem("B", "Unary", unary.Run));
            menu.Run();
        }
        
        public static string Plus()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} + ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            CalculatorCurrentValue = CalculatorCurrentValue + converted;
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }        
        public static string Minus()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} - ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            CalculatorCurrentValue = CalculatorCurrentValue - converted;
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }        
        public static string Division()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} / ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            if (Math.Abs(converted) < 0.000001)
            {
                Console.WriteLine($"You can not divide with zero! You entered {n}");
            }
            else
            {
                CalculatorCurrentValue = CalculatorCurrentValue / converted;
            }
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }
        
        public static string Multiply()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} * ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            CalculatorCurrentValue = CalculatorCurrentValue * converted;
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }        
        
        public static string Power()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} power ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            CalculatorCurrentValue = Math.Pow(CalculatorCurrentValue, converted);
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }        
        
        public static string Negate()
        {
            CalculatorCurrentValue = CalculatorCurrentValue * -1;
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }
                
        public static string Absolute()
        {
            CalculatorCurrentValue = Math.Abs(CalculatorCurrentValue);
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }     
        
        public static string SquareRoot()
        {
            if (CalculatorCurrentValue >= 0)
            {
                CalculatorCurrentValue = Math.Sqrt(CalculatorCurrentValue);
            }
            else
            {
                Console.WriteLine("You can not take square root out of negative number!");
            }
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }
        public static string Square()
        {
            CalculatorCurrentValue = Math.Pow(CalculatorCurrentValue, 2);
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }
    }
}