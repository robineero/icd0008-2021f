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
                List<Player> player = _context.Players.Where(p => p.GameId == gameId).ToList();
                Game game = _context.Games.FirstOrDefault(g => g.Id == gameId)!;
                game.HasStarted = true;
                game.UpdatedAt = DateTime.Now;
                //
                Player current = player.FirstOrDefault(nm => nm.NextMove)!;
                Player next = player.FirstOrDefault(nm => !nm.NextMove)!;
                PlayerCurrent = current;
                current.NextMove = !current.NextMove;
                next.NextMove = !next.NextMove;
                //
                Board = JsonSerializer.Deserialize<Board>(next.Board!)!;
                Board.PlaceBomb(x, y);
                next.Board = JsonSerializer.Serialize(Board);
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
