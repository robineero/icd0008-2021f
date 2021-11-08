using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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

        public List<string>? Board { get; set; }
        public int GameId { get; set; }
        
        public IActionResult OnGetAsync(int id, int x, bool move)
        {
            GameId = id;
            if (move)
            {
                List<Player> player = _context.Players.Where(p => p.GameId == id).ToList();
                Game game = _context.Games.FirstOrDefault(g => g.Id == id)!;
                game.UpdatedAt = DateTime.Now;
                
                Player current = player.FirstOrDefault(nm => nm.NextMove == true)!;
                Player next = player.FirstOrDefault(nm => nm.NextMove == false)!;
                
                Board = JsonSerializer.Deserialize<List<string>>(current.Board!);
                Board![x] = "x";
                current.Board = JsonSerializer.Serialize(Board);
                current.NextMove = !current.NextMove;
                next.NextMove = !next.NextMove;
                _context.SaveChanges();
                
                return RedirectToPage("/Games/Index");
            }
            else
            {
                Player player = _context.Players.FirstOrDefault(x => x.GameId == id && x.NextMove == true)!;
                Board = JsonSerializer.Deserialize<List<string>>(player.Board!);
            
                return Page();
            }
        }
    }
}
