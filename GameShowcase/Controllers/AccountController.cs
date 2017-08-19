using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameShowcase.Models;
using GameShowcase.Models.AccountViewModels;
using Microsoft.EntityFrameworkCore;
using GameShowcase.Data;
using Microsoft.AspNetCore.Http;

namespace GameShowcase.Controllers
{
    public class AccountController : UseSessionController
    {
        private readonly GameDbContext _context;

        public AccountController(GameDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LeaderLoginViewModel loginVM)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (ModelState.IsValid)
            {
                var leader = await _context.Leaders
                    .SingleOrDefaultAsync(l => l.StudentNumber == loginVM.StuNumber);

                if (leader == null)
                {
                    ModelState.AddModelError(string.Empty, "不存在该学号的组长");
                    return View(loginVM);
                }

                if (leader.Password == loginVM.Password)
                {
                    CurrentAccountData.LeaderLogin(leader);
                    return RedirectToAction("Index", "Team", new { area = "Manage" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "密码错误");
                }
            }

            return View(loginVM);
        }

        public IActionResult AdminLogin()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            return View();
        }

        [HttpPost]
        [ActionName("AdminLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLoginAsync(AdminLoginViewModel loginVM)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            if (ModelState.IsValid)
            {
                var admin = await _context.Admins
                    .SingleOrDefaultAsync(a => a.Password == loginVM.Password);

                if (admin != null)
                {
                    CurrentAccountData.AdminLogIn(admin);
                    return RedirectToAction("Index", "Leaders", new { area = "Manage" });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "密码错误");
                }
            }

            return View(loginVM);
        }

        public IActionResult SignOff()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            CurrentAccountData.SignOff();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ModifyPassword()
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            ViewData["ModifySuccess"] = false;
            if (!CurrentAccountData.LoggedIn)
            {
                return View("NoPermission");
            }

            return View("ModifyPassword");
        }
        
        [HttpPost, ActionName("ModifyPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModifyPasswordAsync(ModifyPasswordViewModel viewModel)
        {
            ViewData["CurrentAccountData"] = CurrentAccountData;
            ViewData["ModifySuccess"] = false;
            if (!CurrentAccountData.LoggedIn)
            {
                return View("NoPermission");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (CurrentAccountData.IsAdmin)
                    {
                        var account = await _context.Admins
                            .SingleOrDefaultAsync(a => a.Id == CurrentAccountData.LoggedId);

                        if (account.Password != viewModel.OriginPassword)
                        {
                            ModelState.AddModelError(string.Empty, "原密码错误");
                            return View("ModifyPassword", viewModel);
                        }

                        account.Password = viewModel.NewPassword;
                        _context.Update(account);
                    }
                    else
                    {
                        var account = await _context.Leaders
                            .SingleOrDefaultAsync(a => a.Id == CurrentAccountData.LoggedId);

                        if (account.Password != viewModel.OriginPassword)
                        {
                            ModelState.AddModelError(string.Empty, "原密码错误");
                            return View("ModifyPassword", viewModel);
                        }

                        account.Password = viewModel.NewPassword;
                        _context.Update(account);
                    }
                    _context.SaveChanges();
                    ViewData["ModifySuccess"] = true;
                    return View("ModifyPassword");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "发生错误，请稍后再试");
                }
            }

            return View("ModifyPassword", viewModel);
        }
    }
}