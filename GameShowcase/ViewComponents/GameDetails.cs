using GameShowcase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.ViewComponents
{
    public class GameDetails : ViewComponent
    {
        private readonly GameDbContext _context;

        public GameDetails(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var game = await _context.Games
                .Include(g => g.Leader)
                .Include(g => g.Screenshots)
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == id);

            return View(game);
        }
    }
}
