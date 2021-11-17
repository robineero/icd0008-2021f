using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BattleshipBrain;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Games.Play
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public Board Board { get; set; } = default!;
        public Player PlayerCurrent { get; set; } = default!;
        public Player PlayerOpponent { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int gameId, int x, int y, bool move)
        {
            if (move)
            {
                // Load players
                List<Player> players = _context.Players.Where(p => p.GameId == gameId).ToList();
                Player current = players.FirstOrDefault(nm => nm.NextMove)!;
                Player opponent = players.FirstOrDefault(nm => !nm.NextMove)!;
                
                // Get the game, update dates
                Game game = _context.Games.FirstOrDefault(g => g.Id == gameId)!;
                game.StartDate = DateTime.Now;
                game.UpdatedAt = DateTime.Now;
                
                // Check if cell already has bomb
                Board opponentsBoard = JsonSerializer.Deserialize<Board>(opponent.Board!)!;
                var bss = opponentsBoard.Rows[y].Coordinates[x].BoardSquareState;
                if (bss.IsBomb)
                {
                    PlayerCurrent = current;
                    PlayerOpponent = opponent;
                    return Page();
                }
                
                // Switch players if is not hit
                if (!bss.IsShip) 
                {
                    current.NextMove = !current.NextMove;
                    opponent.NextMove = !opponent.NextMove;
                }
                
                //opponentsBoard.PlaceBomb(x, y);
                Board = JsonSerializer.Deserialize<Board>(opponent.Board!)!;
                Board.PlaceBomb(x, y);
                opponent.Board = JsonSerializer.Serialize(Board);
                await _context.SaveChangesAsync();
                
                return RedirectToPage("/Games/Index");
            }
            else
            {
                Console.WriteLine($"Tuleb siia ja {gameId}");
                PlayerCurrent = await _context.Players.FirstOrDefaultAsync(p => p.GameId == gameId && p.NextMove == true)!;
                PlayerOpponent = await _context.Players.FirstOrDefaultAsync(p => p.GameId == gameId && p.NextMove == false)!;
                
                return Page();
            }
        }
    }
}
