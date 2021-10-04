using System;
using BattleShipBrain;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Brain brain = new Brain(5, 15);
            Console.WriteLine("Hello World!");
            
            Console.WriteLine(brain.GetBoardA());
        }
    }
}