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
            Console.Write("Continue previous game (Y/N): ");
            String continuePrevious = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (continuePrevious == "y")
            {
                _playerA = DeserializeGame()!.First(x => x.Code == "A");
                _playerB = DeserializeGame()!.First(x => x.Code == "B");
            }
            else
            {
                Config config = CreateConfig();
            
                _playerA = new("A","PlayerA", new Board(config), true);
                _playerA.Board.SetupNewBoard();
                _playerB = new("B","PlayerB", new Board(config));
                
            }

            
            do
            {
                _currentPlayer = _playerA.MyTurn ? _playerA : _playerB;
                SwitchCurrentPlayer();
                
                Console.WriteLine($"\n{_currentPlayer.Name}, place your bomb.");
                Console.WriteLine(_currentPlayer.Board);
                
                do
                {
                    int col;
                    int row;
                    Console.Write("Col: ");
                    Int32.TryParse(Console.ReadLine()?.Trim(), out col);
                    Console.Write("Row: ");
                    Int32.TryParse(Console.ReadLine()?.Trim(), out row);
                    
                    if (row < _currentPlayer.Board.Height && col < _currentPlayer.Board.Width && row >= 0 && col >= 0)
                    {
                        String feedback = _currentPlayer.Board.PlaceBomb(col,row);
                        if (feedback != "")
                        {
                            Console.WriteLine(feedback);
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Something went wrong with the inputs. Please choose again.");
                    }
                    
                } while (true);

                SerializeGame(); // Autosave current state
                
                Console.Write("Continue playing this game (Y/N): ");
                String continuePlay = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (continuePlay == "n")
                {
                    loadingMessage("Closing the game");
                    break;
                }
                loadingMessage("Loading next player's board");

            } while (true);
        }
        

        private Config CreateConfig()
        {
            Console.Write("Board width: ");
            int width;
            Int32.TryParse(Console.ReadLine()?.Trim(), out width);
            
            Console.Write("Board height: ");
            int height;
            Int32.TryParse(Console.ReadLine()?.Trim(), out height);
            
            return new Config (width <= 5 ? 5: width, height <= 5 ? 5 : height);
        }

        private void SwitchCurrentPlayer()
        {
            _playerA.MyTurn = !_playerA.MyTurn;
            _playerB.MyTurn = !_playerB.MyTurn;
        }

        private void loadingMessage(String what)
        {
            Console.Write($"{what}");
                
            for (int i = 0; i < 4; i++)
            {
                Console.Write(".");
                Thread.Sleep(600);
            }
            
            Console.WriteLine("\n\n");
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