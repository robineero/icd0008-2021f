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
        private int _gameId = 0;
        
        public void Run()
        {
            Console.Write("Continue previous game (Y/N*): ");
            String continuePrevious = Console.ReadLine()?.Trim().ToLower() ?? "";

            if (continuePrevious == "y") // Load previous
            {
                Console.WriteLine("Id\tLast played");
                foreach (var game in LoadGamesFromDatabase())
                    Console.WriteLine($"{game.Id})\t{game.UpdatedAt}");
                Console.Write("Which game do you want to play: ");
                Int32.TryParse(Console.ReadLine()?.Trim(), out var choice);

                LoadPlayersFromDatabase(choice);
                
                Console.WriteLine("Starting to play the game with Id: " + _gameId);
            }
            else // N - new game
            {
                Config config = CreateConfig();
            
                _playerA = new("A","PlayerA", new Board(config), true);
                _playerA.Board.SetupNewBoard();
                _playerB = new("B","PlayerB", new Board(config));
                
                SaveGameToDatabase();
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
                SaveGameToDatabase();

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

        private void SaveGameToDatabase()
        {
            using var db = new AppDbContext();
            if (_gameId > 0) // Update game
            {
                var game = db.Games.FirstOrDefault(x => x.Id == _gameId);
                if (game == null) return;
                game.UpdatedAt = DateTime.Now;
                db.SaveChanges();
                SaveSerializedPlayers(_gameId);
            }
            else // Create new game
            {
                var game = new Game()
                {
                    UpdatedAt = DateTime.Now
                };
                db.Games.Add(game);
                db.SaveChanges();
                _gameId = game.Id;
                SaveSerializedPlayers(game.Id);
            }
        }
        
        private void LoadPlayersFromDatabase(int id)
        {
            _gameId = id;
            using var db = new AppDbContext();
            List<Player> players = new();
            var query = db.Players.Where(x => x.GameId == id);
            foreach (var q in query)
            {
                if (q.PlayerString != null) players.Add(JsonSerializer.Deserialize<Player>(q.PlayerString)!);

            }
            _playerA = players.First(x => x.Code == "A");
            _playerB = players.First(x => x.Code == "B");
            
            // throw new Exception("Game file is not available for deserialization");
        }

        private List<Game> LoadGamesFromDatabase()
        {
            using var db = new AppDbContext();
            var result = db.Games.OrderByDescending(x => x.UpdatedAt).Take(3).ToList();
            return result;
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
        
        
        private void SaveSerializedPlayers(int gameId)
        {
            using var db = new AppDbContext();
            var players = db.Players.Where(x => x.GameId == gameId);
            
            if (players.Count() != 0) // remove old entries if exist
                foreach (var p in players) db.Players.Remove(p);
            
            db.Players.Add(new Domain.Player()
            {
                PlayerString = JsonSerializer.Serialize(_playerA),
                GameId = gameId
            });
            db.Players.Add(new Domain.Player()
            {
                PlayerString = JsonSerializer.Serialize(_playerB),
                GameId = gameId
            });
            db.SaveChanges();
        }
    }
}