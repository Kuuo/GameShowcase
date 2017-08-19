using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShowcase.Models;
using GameShowcase.Data;
using GameShowcase.Areas.Manage.Models.ViewModels;
using GameShowcase.Controllers;

namespace GameShowcase.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class LeadersController : UseSessionController
    {
        private readonly GameDbContext _context;

        public LeadersController(GameDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            var leaders = await _context.Leaders
                .Include(leader => leader.Game)
                .ToListAsync();

            return View(leaders);
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            if (id == null)
            {
                return NotFound();
            }

            var leader = await _context.Leaders
                .Include(l => l.Game)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (leader == null)
            {
                return NotFound();
            }

            return View(leader);
        }

        public IActionResult Create()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaderCEViewModel leaderViewModel)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            if (ModelState.IsValid)
            {
                _context.Add(new Leader(leaderViewModel));
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(leaderViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            if (id == null)
            {
                return NotFound();
            }

            var leader = await _context.Leaders
                .SingleOrDefaultAsync(m => m.Id == id);

            if (leader == null)
            {
                return NotFound();
            }

            return View(new LeaderCEViewModel(leader));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaderCEViewModel viewModel)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leader = await _context.Leaders.SingleAsync(l => l.Id == id);
                    leader.Update(viewModel);
                    _context.Update(leader);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaderExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            if (id == null)
            {
                return NotFound();
            }

            var leader = await _context.Leaders
                .SingleOrDefaultAsync(m => m.Id == id);
            if (leader == null)
            {
                return NotFound();
            }

            return View(leader);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsAdmin)
            {
                return View("NoPermission");
            }

            var leader = await _context.Leaders.SingleOrDefaultAsync(m => m.Id == id);
            _context.Leaders.Remove(leader);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool LeaderExists(int id)
        {
            return _context.Leaders.Any(e => e.Id == id);
        }
    }
}
