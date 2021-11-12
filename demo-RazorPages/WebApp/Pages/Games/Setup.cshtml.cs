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

        public IActionResult OnGet(int playerId)
        {
            Player = _context.Players.FirstOrDefault(x => x.Id == playerId)!;
            Game game = _context.Games.FirstOrDefault(x => x.Id == Player.GameId)!;
            if (game.StartDate == null)
            {
                Board = JsonSerializer.Deserialize<Board>(Player.Board!)!;
                return Page();
            }
            return RedirectToPage("./Index");
        }

        [BindProperty] public List<string>? Ships { get; set;  }
        [BindProperty] public bool CreateRandomBoard { get; set; }
        [BindProperty] public int? PlayerId { get; set; }

        public async Task<IActionResult> OnPost()
        {
            Console.WriteLine($"CreateRandomBoard: {CreateRandomBoard}");
            Console.WriteLine($"Count: {Ships!.Count}.  {string.Join(", ", Ships)}");
            Console.WriteLine($"Player id: {PlayerId}");
            
            Player = await _context
                .Players
                .Include(x => x.Game)
                .FirstOrDefaultAsync(x => x.Id == PlayerId)!;


            if (CreateRandomBoard)
            {
                Config conf = new (Player.Game!.BoardSize, Player.Game!.BoardSize, CreateRandomBoard);
                Board = new Board(conf);
                Player.Board = JsonSerializer.Serialize(Board);
                await _context.SaveChangesAsync();
                return Page();
            }
            
            if (Ships.Count == 15) // correct number of ships
            {
                Config conf = new (Player.Game!.BoardSize, Player.Game!.BoardSize);
                Board = new Board(conf);

                foreach (var ship in Ships)
                {
                    var coords = ship.Split("-");
                    int x = Int32.Parse(coords[0]);
                    int y = Int32.Parse(coords[1]);

                    Board.PlaceShip(x, y);
                }

                Player.Board = JsonSerializer.Serialize(Board);
                await _context.SaveChangesAsync();
            } 
            else 
            {
                Config conf = new (Player.Game!.BoardSize, Player.Game!.BoardSize);
                Board = new Board(conf);

                foreach (var ship in Ships)
                {
                    var coords = ship.Split("-");
                    int x = Int32.Parse(coords[0]);
                    int y = Int32.Parse(coords[1]);

                    Board.PlaceShip(x, y);
                }

                Player.Board = JsonSerializer.Serialize(Board);
                return Page();
            }
            
            return RedirectToPage("./Index");
        }
    }
}