using System;
using System.Threading;

namespace CalculatorBrain
{
    public static class UnaryMethods
    {
        
        public static string Negate()
        {
            Console.Write($"Calculate: {CalculatorCurrentValue} power ");
            var n = Console.ReadLine()?.Trim();
            double.TryParse(n, out var converted);
            CalculatorCurrentValue = Math.Pow(CalculatorCurrentValue, converted);
            Console.WriteLine($"Result: {CalculatorCurrentValue}");
            Thread.Sleep(2000);
            
            return "";
        }
        
    }
}