using System;
using System.Collections.Generic;
using System.Threading;

namespace BattleShipBrain
{
    public class Brain
    {
        private Player _currentPlayer = default!;
        private Player _playerA = default!;
        private Player _playerB = default!;

        public void Run()
        {
            // Console.Write("Board width: ");
            int width = 15;
            // Int32.TryParse(Console.ReadLine()?.Trim(), out width);
            // Console.Write("Board height: ");
            int height = 5;
            //Int32.TryParse(Console.ReadLine()?.Trim(), out height);
            
            Config config = new Config(width, height, false);
            
            _playerA = new("PlayerA", new Board(config));
            _playerB = new("PlayerB", new Board(config));
            
            
            // Adding one ship test
            List<Coordinate> coordinates = new();
            coordinates.Add(new Coordinate() { X= 0, Y = 0});
            _playerA.Board.AddShip(new Ship("Patrol", coordinates));

            do
            {
                _currentPlayer = _currentPlayer == _playerA ?  _playerB : _playerA;
                Console.WriteLine($"\n{_currentPlayer.Name}, place your bomb.");
                Console.WriteLine(_currentPlayer.Board);
                int col;
                int row;
                
                do
                {
                    Console.Write("Col: ");
                    Int32.TryParse(Console.ReadLine()?.Trim(), out col);
                    Console.Write("Row: ");
                    Int32.TryParse(Console.ReadLine()?.Trim(), out row);

                    if (row < height && col < width && row >= 0 && col >= 0)
                    {
                        String feedback = _currentPlayer.Board.PlaceBomb(col,row);
                        Console.WriteLine(feedback);
                        break;
                    }

                    Console.WriteLine("Something went wrong with the inputs. Please choose again.");
                    
                } while (true);
                
                for (int i = 0; i < _currentPlayer.Board.Width * 3; i++)
                {
                    Console.Write("= ");
                    Thread.Sleep(15);
                }
                
            } while (true);
        }
    }
}