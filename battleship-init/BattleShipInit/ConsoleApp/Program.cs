using System;
using GameBoard;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Board board = new Board(10, 10);
            
            Console.WriteLine(board);
            board.PlaceBomb(4,4);
            Console.WriteLine(board);
        }
    }
}