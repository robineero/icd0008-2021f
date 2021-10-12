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
            
            _playerA = new("PlayerA", new Board(config), true);
            _playerB = new("PlayerB", new Board(config));
            
            // Adding one ship test
            ShipFactory shipFactory = new ();
            Ship patrol = shipFactory.GetPatrol(1, 1, ShipDirection.NorthSouth);
            Ship submarine = shipFactory.GetSubmarine(3, 0, ShipDirection.EastWest);
            _playerA.Board.AddShip(patrol);
            _playerA.Board.AddShip(submarine);

            do
            {
                _currentPlayer = _playerA.MyTurn ? _playerA : _playerB;
                SwitchCurrentPlayer();
                
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

        private void SwitchCurrentPlayer()
        {
            _playerA.MyTurn = !_playerA.MyTurn;
            _playerB.MyTurn = !_playerB.MyTurn;
        }
    }
}