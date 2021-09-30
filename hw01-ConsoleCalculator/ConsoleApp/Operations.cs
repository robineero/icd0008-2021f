using System;
using System.Threading;
using static ConsoleApp.Program;

namespace ConsoleApp
{
    public class Operations
    {
        public static void PrintResult()
        {
            Console.WriteLine($"Result: {GetCalculatorValue()}");
            Thread.Sleep(2000);
        }
        public static string Plus()
        {
            var currentValue = GetCalculatorValue();
            Console.Write($"Calculate: {currentValue} + ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            SetCalculatorValue(currentValue + converted);
            PrintResult();
            return "";
        }  
        
        public static string Minus()
        {
            var currentValue = GetCalculatorValue();
            Console.Write($"Calculate: {currentValue} - ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            SetCalculatorValue(currentValue - converted);
            PrintResult();
            return "";
        }        
        public static string Division()
        {
            var currentValue = GetCalculatorValue();
            Console.Write($"Calculate: {currentValue} / ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            if (Math.Abs(converted) < 0.000001)
            {
                Console.WriteLine($"You can not divide with zero! You entered {n}");
            }
            else
            {
                SetCalculatorValue(currentValue / converted); ;
            }
            PrintResult();
            return "";
        }
        
        public static string Multiply()
        {
            var currentValue = GetCalculatorValue();
            Console.Write($"Calculate: {currentValue} * ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            SetCalculatorValue(currentValue * converted);
            PrintResult();
            return "";
        }        
        
        public static string Power()
        {
            var currentValue = GetCalculatorValue();
            Console.Write($"Calculate: {currentValue} power ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            SetCalculatorValue(Math.Pow(currentValue, converted));
            PrintResult();
            return "";
        }  
    }
}