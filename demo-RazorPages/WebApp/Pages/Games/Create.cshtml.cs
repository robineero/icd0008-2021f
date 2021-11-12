using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BattleshipBrain;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public Config Config { get; set; } = default!;
        [BindProperty] public Game Game { get; set; } = default!;
        [BindProperty] public Player PlayerA { get; set; } = default!;
        [BindProperty] public Player PlayerB { get; set; } = default!;
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Games.Add(Game);
            await _context.SaveChangesAsync();
            
            Config = new Config(Game.BoardSize, Game.BoardSize);
            Board board = new(Config);
            
            
            PlayerA.GameId = Game.Id;
            PlayerA.Code = 'A';
            PlayerA.Board = JsonSerializer.Serialize(board);
            PlayerA.NextMove = true;
            _context.Players.Add(PlayerB);
            
            PlayerB.GameId = Game.Id;
            PlayerB.Code = 'B';
            PlayerB.Board = JsonSerializer.Serialize(board);
            _context.Players.Add(PlayerA);
            
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        
    }
}
