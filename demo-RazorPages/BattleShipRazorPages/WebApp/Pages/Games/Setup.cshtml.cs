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

namespace WebApp.Pages.Games
{
    public class Setup : PageModel
    {
        private readonly AppDbContext _context;
        public Board Board { get; set; } = default!;
        public Player Player { get; set; } = default!;
        
        public Setup(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id, int playerId)
        {
            Player = _context.Players.FirstOrDefault(x => x.Id == playerId)!;
            Board = JsonSerializer.Deserialize<Board>(Player.Board!)!;
            
            return Page();
        }

        [BindProperty] public List<string>? Ships { get; set;  }
        [BindProperty] public string? TestCheckbox { get; set; }
        [BindProperty] public int? PlayerId { get; set; }

        public IActionResult OnPost()
        {
            Console.WriteLine($"Test: {TestCheckbox}");
            Console.WriteLine($"Count: {Ships!.Count}.  {string.Join(", ", Ships)}");
            Console.WriteLine($"Player id: {PlayerId}");

            if (Ships.Count != 0)
            {
                Player = _context.Players.FirstOrDefault(x => x.Id == PlayerId)!;
                Board = JsonSerializer.Deserialize<Board>(Player.Board!)!;

                foreach (var ship in Ships)
                {
                    var coords = ship.Split("-");
                    int x = Int32.Parse(coords[0]);
                    int y = Int32.Parse(coords[1]);

                    Board.PlaceShip(x, y);
                }

                Player.Board = JsonSerializer.Serialize(Board);
                _context.SaveChanges();
            }
            
            return RedirectToPage("./Index");
        }
    }
}