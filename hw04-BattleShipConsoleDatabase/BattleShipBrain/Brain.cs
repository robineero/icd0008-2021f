using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using DAL;
using Domain;

namespace BattleShipBrain
{
    public class Brain
    {
        private Player _currentPlayer = default!;
        private Player _playerA = default!;
        private Player _playerB = default!;

        public void Run()
        {
            Console.Write("Continue previous game (Y/N): ");
            String continuePrevious = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (continuePrevious == "y")
            {
                _playerA = LoadGameFromDatabase()!.First(x => x.Code == "A");
                _playerB = LoadGameFromDatabase()!.First(x => x.Code == "B");
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
                
                // Autosave current state
                SaveGameToDatabase(SerializeGame());
                LoadGameFromDatabase();
                
                Console.Write("Continue playing this game (Y/N): ");
                String continuePlay = Console.ReadLine()?.Trim().ToLower() ?? "";
                if (continuePlay == "n")
                {
                    LoadingMessage("Closing the game");
                    break;
                }
                LoadingMessage("Loading next player's board");

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

        private void SaveGameToDatabase(string currentGame)
        {
            using var db = new AppDbContext();
            if (!db.Games.Any())
            {
                db.Games.Add(new Game()
                {
                    GameString = currentGame,
                    UpdatedAt = DateTime.Now
                });
                db.SaveChanges();
            }
            else
            {
                var game = db.Games.FirstOrDefault();
                if (game == null) return;
                game.GameString = currentGame;
                game.UpdatedAt = DateTime.Now;
                db.SaveChanges();
            }
        }
        
        private List<Player>? LoadGameFromDatabase()
        {
            using var db = new AppDbContext();

            var gameString = db.Games.FirstOrDefault()?.GameString ?? null;
            if (gameString != null)
            {
                List<Player>? game = JsonSerializer.Deserialize<List<Player>>(gameString);
                return game;
            }
            
            throw new Exception("Game file is not available for deserialization");
        }
        
        private void SwitchCurrentPlayer()
        {
            _playerA.MyTurn = !_playerA.MyTurn;
            _playerB.MyTurn = !_playerB.MyTurn;
        }

        private void LoadingMessage(String what)
        {
            Console.Write($"{what}");
                
            for (int i = 0; i < 4; i++)
            {
                Console.Write(".");
                Thread.Sleep(600);
            }
            
            Console.WriteLine("\n\n");
        }

        private string SerializeGame()
        {
            List<Player> game = new()
            {
                _playerA, _playerB
            };

            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = false
            };
            
            return JsonSerializer.Serialize(game, jsonOptions);
        }
        
    }
}