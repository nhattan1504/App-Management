﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementApp.Data;
using ManagementApp.Models;
using AppContext = ManagementApp.Models.AppContext;
using ManagementApp.WorkOfUnit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Http;

namespace ManagementApp.Controllers
{
    
    public class UsersController : Controller
    {
        UnitOfWork uow;
        //private readonly AppContext _context;

        public UsersController(IServiceProvider provider) {
            uow = new UnitOfWork(provider);
            }
        //public UsersController() {
        //    uow = new UnitOfWork(new AppContext());
        //    }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            var list = uow.Users.GetAll();
            return View(list);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users
            //    .Include(s => s.Postss).AsNoTracking()
            //    .FirstOrDefaultAsync(m => m.id == id);
            var user = uow.Users.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,email,password,id,isAdmin")] User user)
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            if (ModelState.IsValid)
            {
                //_context.Add(user);
                uow.Users.Add(user);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            if (id == null)
            {
                return NotFound();
            }

            var user = uow.Users.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("name,email,password,id,isAdmin")] User user)
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    uow.Users.Update(user);
                    //_context.Update(user);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!UserExists(user.id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            if (id == null)
            {
                return NotFound();
            }

            //var user = await _context.Users
            //    .FirstOrDefaultAsync(m => m.id == id);
            var user = uow.Users.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            //var user = await _context.Users.FindAsync(id);
            var user = uow.Users.Get(id);
            //_context.Users.Remove(user);
            uow.Users.Remove(user);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Login() {
            //var all = uow.Users.GetAll().Where(x=> x.email==user.email&&x.password==user.password).FirstOrDefault();
            //if (all != null)
            //    {
            //    return RedirectToAction(nameof(Index));
            //    }
            return View();
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user) {
            var all = uow.Users.GetAll().Where(x => (x.email == user.email && x.password == user.password)).FirstOrDefault();
            if (all != null)
                {
                HttpContext.Session.SetString("username", all.name);
                return RedirectToAction(nameof(Index));
                }
            //return View(user);
            return NotFound();
            }
        [Route("logout")]
        [HttpGet]
        public IActionResult Logout() {
            HttpContext.Session.Remove("username");
            return Redirect("Home");
            }
        //private bool UserExists(int id)
        //{
        //    return uow.Users.Any(e => e.id == id);
        //}
        }
}
