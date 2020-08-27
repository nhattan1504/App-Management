using System;
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

namespace ManagementApp.Controllers {
    public class UsersController : Controller {
        UnitOfWork uow;
        //private readonly AppContext _context;

        public UsersController(IServiceProvider provider) {
            uow = new UnitOfWork(provider);
            }
        //public UsersController() {
        //    uow = new UnitOfWork(new AppContext());
        //    }
        // GET: Users
        //[Route("login")]
        public IActionResult Login() {
            if (HttpContext.Session.GetString("username") != null)
                {
                return NotFound();
                }
            return View();
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user) {
            var all = uow.Users.GetAll().Where(x => (x.email == user.email && x.password == user.password)).FirstOrDefault();
            if (all != null)
                {
                HttpContext.Session.SetString("username", all.name);
                if (all.isAdmin)
                    {
                    return Redirect("/admin/index");
                    }
                else return Redirect("/user/index");
                //return RedirectToAction(nameof(Index));
                
            //return RedirectToAction(nameof(Index));
            }
            //return View(user);
            return NotFound();
            }

        public IActionResult Register() {
            //var all = uow.Users.GetAll().Where(x=> x.email==user.email&&x.password==user.password).FirstOrDefault();
            //if (all != null)
            //    {
            //    return RedirectToAction(nameof(Index));
            //    }
            return View();
            }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("name,email,password,id,isAdmin=false")] ManagementApp.Models.User user) {
            if (ModelState.IsValid)
                {
                //_context.Add(user);
                uow.Users.Add(user);
                //await _context.SaveChangesAsync();
                return Redirect("Login");
                }
            return View(user);
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
