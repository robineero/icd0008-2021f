using System;
using System.Collections.Generic;
using System.Text.Json;
using BattleShipBrain;
using DAL;
using Domain;

namespace ConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Game stuff
            Brain brain = new ();
            brain.Run();
            
            // using var db = new AppDbContext();
            // var game = new Game();
            // db.Games.Add(game);
            // Console.WriteLine(game.Id);
            // db.SaveChanges();
            // Console.WriteLine(game.Id);
        }
    }
}