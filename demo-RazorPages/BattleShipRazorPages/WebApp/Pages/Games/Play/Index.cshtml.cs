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

        public Board Board { get; set; } = default!;
        public Player Player { get; set; } = default!;
        public Player PlayerCurrent { get; set; } = default!;
        public Player PlayerOpponent { get; set; } = default!;

        public IActionResult OnGetAsync(int id, int x, int y, bool move)
        {
            if (move)
            {
                List<Player> player = _context.Players.Where(p => p.GameId == id).ToList();
                Game game = _context.Games.FirstOrDefault(g => g.Id == id)!;
                game.UpdatedAt = DateTime.Now;
                //
                Player current = player.FirstOrDefault(nm => nm.NextMove)!;
                Player next = player.FirstOrDefault(nm => !nm.NextMove)!;
                Player = current;
                current.NextMove = !current.NextMove;
                next.NextMove = !next.NextMove;
                //
                Board = JsonSerializer.Deserialize<Board>(next.Board!)!;
                Board.PlaceBomb(x, y);
                next.Board = JsonSerializer.Serialize(Board);
                _context.SaveChanges();
                
                return RedirectToPage("/Games/Index");
            }
            else
            {
                PlayerCurrent = _context.Players.FirstOrDefault(x => x.GameId == id && x.NextMove == true)!;
                PlayerOpponent = _context.Players.FirstOrDefault(x => x.GameId == id && x.NextMove == false)!;
                
                return Page();
            }
        }
    }
}
