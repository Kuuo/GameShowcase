using GameShowcase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShowcase.ViewComponents
{
    public class ExistDevYearList : ViewComponent
    {
        private readonly GameDbContext _context;

        public ExistDevYearList(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _context.Leaders
                .Select(l => l.Year)
                .Distinct()
                .OrderByDescending(y => y)
                .ToListAsync();

            return View(list);
        }
    }
}
