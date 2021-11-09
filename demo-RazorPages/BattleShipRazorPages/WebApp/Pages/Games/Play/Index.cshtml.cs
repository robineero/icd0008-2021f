using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using BattleshipBrain;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Games.Play
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public BattleshipBrain.Board Board { get; set; } = default!;
        public int GameId { get; set; }
        
        public IActionResult OnGetAsync(int id, int x, int y, bool move)
        {
            GameId = id;
            if (move)
            {
                List<Player> player = _context.Players.Where(p => p.GameId == id).ToList();
                Game game = _context.Games.FirstOrDefault(g => g.Id == id)!;
                game.UpdatedAt = DateTime.Now;
                //
                Player current = player.FirstOrDefault(nm => nm.NextMove == true)!;
                Player next = player.FirstOrDefault(nm => nm.NextMove == false)!;
                current.NextMove = !current.NextMove;
                next.NextMove = !next.NextMove;
                //
                Board = JsonSerializer.Deserialize<Board>(current.Board!)!;
                Board.PlaceBomb(x, y);
                current.Board = JsonSerializer.Serialize(Board);
                _context.SaveChanges();
                
                return RedirectToPage("/Games/Index");
            }
            else
            {
                Player player = _context.Players.FirstOrDefault(x => x.GameId == id && x.NextMove == true)!;
                String board = player.Board!;
                Board = JsonSerializer.Deserialize<Board>(board)!;
            
                return Page();
            }
        }
    }
}
