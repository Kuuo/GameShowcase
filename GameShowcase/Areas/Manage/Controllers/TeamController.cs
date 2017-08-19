using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameShowcase.Controllers;
using GameShowcase.Models;
using GameShowcase.Data;
using GameShowcase.Areas.Manage.Models.ViewModels;

namespace GameShowcase.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : UseSessionController
    {
        private readonly GameDbContext _context;

        public TeamController(GameDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            int id = CurrentAccountData.LoggedId;

            var gameExists = GameExists(id);

            if (!gameExists)
            {
                return View("GameNotCreated");
            }

            return View(id);
        }

        [ActionName("GameBaseInfo")]
        public async Task<IActionResult> GameBaseInfoAsync()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            var game = await GameOfCurrentTeamAsync();

            if (game == null)
            {
                return View("GameNotCreated");
            }

            return View(new GameBaseInfoViewModel(game));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GameBaseInfo")]
        public async Task<IActionResult> GameBaseInfoAsync(int id, GameBaseInfoViewModel baseInfoVM)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            if (id != baseInfoVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var game = await _context.Games.SingleAsync(g => g.Id == id);
                    game.Update(baseInfoVM);
                    _context.Update(game);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "±£¥Ê ß∞‹£¨«Î…‘∫Û≥¢ ‘");
                    return View(baseInfoVM);
                }
                return RedirectToAction("Index");
            }

            return View(baseInfoVM);
        }

        public IActionResult Create()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GameBaseInfoViewModel baseInfoVM)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var leader = await _context.Leaders
                                        .Include(l => l.Game)
                                        .SingleOrDefaultAsync(l => l.Id == CurrentAccountData.LoggedId);

                    if (leader == null || leader.Game != null)
                    {
                        return NotFound();
                    }

                    leader.Game = new Game(baseInfoVM);
                    _context.Update(leader);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "±£¥Ê ß∞‹£¨«Î…‘∫Û≥¢ ‘");
                    return View(baseInfoVM);
                }
            }
            return RedirectToAction("Index");
        }

        [ActionName("GameIntro")]
        public async Task<IActionResult> GameIntroAsync()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            var game = await GameOfCurrentTeamAsync();

            if (game == null)
            {
                return View("GameNotCreated");
            }

            return View(new GameIntroViewModel(game));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GameIntro")]
        public async Task<IActionResult> GameIntroAsync(int id, GameIntroViewModel viewModel)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
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
                    var game = await _context.Games.SingleAsync(g => g.Id == id);
                    game.Description = viewModel.Content;
                    _context.Update(game);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "±£¥Ê ß∞‹£¨«Î…‘∫Û≥¢ ‘");
                    return View(viewModel);
                }
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [ActionName("GameScreenShots")]
        public async Task<IActionResult> GameScreenShotsAsync()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            var game = await GameOfCurrentTeamWithScreenShotsAsync();

            if (game == null)
            {
                return View("GameNotCreated");
            }

            return View(new GameScreenShotsViewModel(game));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("GameScreenShots")]
        public async Task<IActionResult> GameScreenShotsAsync(GameScreenShotsViewModel viewModel)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }

            try
            {
                var game = await GameOfCurrentTeamWithScreenShotsAsync();
                _context.RemoveRange(game.Screenshots);
                game.Screenshots = viewModel.Parse()?.ToList();
                _context.Update(game);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "±£¥Ê ß∞‹£¨«Î…‘∫Û≥¢ ‘");
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [ActionName("Info")]
        public async Task<IActionResult> InfoAsync()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            ViewData["ModifySuccess"] = false;

            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }


            var leader = await _context.Leaders
                .SingleOrDefaultAsync(l => l.Id == CurrentAccountData.LoggedId);

            return View(leader);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Info([Bind("Id", "Name", "StudentNumber", "Year", "Password", "Email", "CoMembers")]
        Leader leader)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            ViewData["ModifySuccess"] = false;

            if (!CurrentAccountData.IsLeader)
            {
                return View("NoPermission");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leader);
                    _context.SaveChanges();
                    ViewData["ModifySuccess"] = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "±£¥Ê ß∞‹£¨«Î…‘∫Û≥¢ ‘");
                }
            }

            return View(leader);
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        private async Task<Game> GameOfCurrentTeamAsync()
        {
            return await _context.Games
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == CurrentAccountData.LoggedId);
        }

        private async Task<Game> GameOfCurrentTeamWithScreenShotsAsync()
        {
            return await _context.Games
                .Include(g => g.Screenshots)
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == CurrentAccountData.LoggedId);
        }
    }
}
