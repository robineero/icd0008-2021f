using System;
using System.Collections.Generic;
using System.Text.Json;
using BattleShipBrain;

namespace ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Brain brain = new ();
            // brain.Run();

            Tuple<int, int> tuple = new(3, 2);
            String json = JsonSerializer.Serialize(tuple);
            Console.WriteLine(json);
        }
    }
}