using GameShowcase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.ViewComponents
{
    public class LeaderProfile : ViewComponent
    {
        private readonly GameDbContext _context;

        public LeaderProfile(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var leader = await _context.Leaders
                .Include(l => l.Game)
                .AsNoTracking()
                .SingleOrDefaultAsync(l => l.Id == id);

            return View(leader);
        }
    }
}
