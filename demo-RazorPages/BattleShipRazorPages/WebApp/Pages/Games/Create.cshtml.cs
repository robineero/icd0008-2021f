using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
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

            PlayerA.GameId = Game.Id;
            PlayerA.Code = 'A';
            PlayerA.Board = createBoard();
            PlayerA.NextMove = true;
            
            PlayerB.GameId = Game.Id;
            PlayerB.Code = 'B';
            PlayerB.Board = createBoard();
            _context.Players.Add(PlayerA);
            _context.Players.Add(PlayerB);
            
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string createBoard()
        {
            List<string> board = new();
            for (int i = 0; i < 5; i++)
            {
                board.Add("");
            }
            return JsonSerializer.Serialize(board);
        }
    }
}
