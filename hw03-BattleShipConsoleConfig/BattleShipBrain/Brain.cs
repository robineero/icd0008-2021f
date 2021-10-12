using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace BattleShipBrain
{
    public class Brain
    {
        private Player _currentPlayer = default!;
        private Player _playerA = default!;
        private Player _playerB = default!;
        private String _savedGameFilename = "ggu6numaa98isv85omtg.json";

        public void Run()
        {
            // Console.Write("Board width: ");
            int width = 5;
            // Int32.TryParse(Console.ReadLine()?.Trim(), out width);
            // Console.Write("Board height: ");
            int height = 5;
            //Int32.TryParse(Console.ReadLine()?.Trim(), out height);
            
            Config config = new (width, height, false);
            
            _playerA = new("A","PlayerA", new Board(config), true);
            _playerB = new("B","PlayerB", new Board(config));
            
            // Adding one ship test
            ShipFactory shipFactory = new ();
            Ship patrol = shipFactory.GetPatrol(1, 1, ShipDirection.NorthSouth);
            Ship submarine = shipFactory.GetSubmarine(3, 0, ShipDirection.NorthSouth);
            _playerA.Board.AddShip(patrol);
            _playerA.Board.AddShip(submarine);
            
            do
            {
                // Serialization for testing
                SerializeGame();
                _playerA = DeserializeGame()!.First(x => x.Code == "A");
                _playerB = DeserializeGame()!.First(x => x.Code == "B");
                
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
                
                Console.Write("Loading new board");
                
                for (int i = 0; i < 4; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(600);
                }

                Console.WriteLine("\n\n\n");
                
            } while (true);
        }

        private void SwitchCurrentPlayer()
        {
            _playerA.MyTurn = !_playerA.MyTurn;
            _playerB.MyTurn = !_playerB.MyTurn;
        }

        private void SerializeGame()
        {
            List<Player> game = new List<Player>()
            {
                _playerA, _playerB
            };

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            
            var json = JsonSerializer.Serialize(game, jsonOptions);
            System.IO.File.WriteAllText(_savedGameFilename, json);
        }
        
        private List<Player>? DeserializeGame()
        {
            if (System.IO.File.Exists(_savedGameFilename))
            {
                String json = System.IO.File.ReadAllText(_savedGameFilename);
                List<Player>? game = JsonSerializer.Deserialize<List<Player>>(json);
                return game;
            }

            throw new Exception("Game file is not available for deserialization");
        }
    }
}