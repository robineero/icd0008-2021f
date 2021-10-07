using System;
using System.Collections.Generic;
using BattleShipBrain;

namespace ConsoleApp
{
    class Program
    {
        private static List<Player> _players = new();
        static void Main(string[] args)
        {
            // Console.Write("Board width: ");
            // int width;
            // Int32.TryParse(Console.ReadLine()?.Trim(), out width);
            // Console.Write("Board height: ");
            // int height;
            //Int32.TryParse(Console.ReadLine()?.Trim(), out height);
            _players.Add(new("PlayerA", new Board(5, 5)));
            //_players.Add(new("PlayerB", new Board(width, height)));

            for (int i = 0; i < 1; i++)
            {
                _players[i].Board.PlaceBomb(0,0);
                Console.WriteLine(_players[i].Board);
            }

            //Console.WriteLine(brain.GetBoardA());
        }
    }
}