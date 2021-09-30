using System;
using System.Threading;
using MenuSystem;
using MenuSystem.Enum;

namespace ConsoleApp
{
    class Program
    {
        private static double _calculatorCurrentValue = 0.0;
        static void Main(string[] args)
        {
            Menu unary = new(MenuLevel.Level1, "---- Level 1 - choose unary operation"); // negate, sqrt, square, abs value  
            unary.Add(new MenuItem("A", "Negate", Negate)); 
            unary.Add(new MenuItem("B", "Square root", SquareRoot)); 
            unary.Add(new MenuItem("C", "Square", Square)); 
            unary.Add(new MenuItem("D", "Absolute value", Absolute));   
            
            Menu binary = new(MenuLevel.Level1, "---- Level 1 - choose binary operation"); // +, -, /, *, x power y
            binary.Add(new MenuItem("+", "Plus", Operations.Plus)); 
            binary.Add(new MenuItem("-", "Minus", Operations.Minus)); 
            binary.Add(new MenuItem("/", "Divide", Operations.Division)); 
            binary.Add(new MenuItem("*", "Multiply", Operations.Multiply));
            binary.Add(new MenuItem("P", "Power", Operations.Power));
            
            Menu menu = new(MenuLevel.Level0, "---- Level 0 - choose type");
            menu.Add(new MenuItem("A", "Binary", binary.Run));
            menu.Add(new MenuItem("B", "Unary", unary.Run));
            menu.Run();
        }

        public static double GetCalculatorValue()
        {
            return _calculatorCurrentValue;
        }
        public static void SetCalculatorValue(double value)
        {
            _calculatorCurrentValue = value;
        }

        public static string Negate()
        {
            _calculatorCurrentValue = _calculatorCurrentValue * -1;
            Console.WriteLine($"Result: {_calculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }
                
        public static string Absolute()
        {
            _calculatorCurrentValue = Math.Abs(_calculatorCurrentValue);
            Console.WriteLine($"Result: {_calculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }     
        
        public static string SquareRoot()
        {
            if (_calculatorCurrentValue >= 0)
            {
                _calculatorCurrentValue = Math.Sqrt(_calculatorCurrentValue);
            }
            else
            {
                Console.WriteLine("You can not take square root out of negative number!");
            }
            Console.WriteLine($"Result: {_calculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }
        public static string Square()
        {
            _calculatorCurrentValue = Math.Pow(_calculatorCurrentValue, 2);
            Console.WriteLine($"Result: {_calculatorCurrentValue}");
            Thread.Sleep(2000);
            return "";
        }
    }
}