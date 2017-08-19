using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShowcase.Models;
using Microsoft.EntityFrameworkCore;
using GameShowcase.Data;

namespace GameShowcase.Controllers
{
    public class HomeController : UseSessionController
    {
        private readonly GameDbContext _context;

        public HomeController(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _context.Games
                .Include(g => g.Leader)
                .AsNoTracking()
                .ToListAsync();
            
            var gamesByYear = games
                .OrderByDescending(g => g.Leader.Year)
                .GroupBy(g => g.Leader.Year)
                .ToDictionary(g => g.Key, g => g.ToList());

            ViewData["CurrentAccountData"] = CurrentAccountData;

            return View(gamesByYear);
        }

        public IActionResult Error()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            return View();
        }
    }
}
