using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty] public Game Game { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // if (id == null)
            // {
            //     return NotFound();
            // }
            //
            // Game = await _context.Games.FirstOrDefaultAsync(m => m.Id == id);
            //
            // if (Game == null)
            // {
            //     return NotFound();
            // }
            
            await OnPostAsync(id);
            return RedirectToPage("./Index");
            // return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Game = await _context.Games.FindAsync(id);

            if (Game != null)
            {
                var players = _context.Players.Where(x => x.GameId == id);
                foreach (var player in players)
                {
                    _context.Players.Remove(player);
                    
                }
                _context.Games.Remove(Game);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
