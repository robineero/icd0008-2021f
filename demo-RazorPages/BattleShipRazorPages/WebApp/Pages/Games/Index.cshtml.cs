using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<Game> Games { get; set; } = default!;
        public IList<Player>? Players { get; set; }

        public async Task OnGetAsync()
        {
            Games = await _context.Games.OrderByDescending(x => x.UpdatedAt).ToListAsync();
            Players = await _context.Players.ToListAsync();

            foreach (var game in Games)
            {
                game.Players = Players!.Where(x => x.GameId == game.Id).ToList();
            }
        }
    }
}
