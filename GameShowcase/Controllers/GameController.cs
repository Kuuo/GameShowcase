using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShowcase.Models;
using GameShowcase.Data;

namespace GameShowcase.Controllers
{
    public class GameController : UseSessionController
    {
        private readonly GameDbContext _context;

        public GameController(GameDbContext context)
        {
            _context = context;
        }

        // GET: Game/id
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;

            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .Include(g => g.Leader)
                .Where(g => g.Leader.Year == id)
                .AsNoTracking()
                .ToListAsync();

            ViewData["DevYear"] = id.Value;

            return View(games);
        }

        // GET: Game/Details/1
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;

            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(g => g.Leader)
                .Include(g => g.Screenshots)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            ViewData["Title"] = game.Title;

            return View(game);
        }
    }
}
