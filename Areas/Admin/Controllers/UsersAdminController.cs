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
using Microsoft.AspNetCore.Routing;

namespace ManagementApp.Areas.Admin.Controllers {
    [Area("Admin")]
    [Route("admin")]
    public class UsersAdminController : Controller {
        UnitOfWork uow;
        //private readonly AppContext _context;

        public UsersAdminController(IServiceProvider provider) {
            uow = new UnitOfWork(provider);
            }
        //public UsersController() {
        //    uow = new UnitOfWork(new AppContext());
        //    }
        
        // GET: Users
        [Route("user")]
        public async Task<IActionResult> Index() {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
                {
                return NotFound();
                }
            var list = uow.Users.GetAll();
            return View("~/Areas/Admin/Views/User/Index.cshtml",list);
            }

        // GET: Users/Details/5
        [Route("user/detail/{id}")]
        public async Task<IActionResult> Details(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
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

            return View("~/Areas/Admin/Views/User/Details.cshtml",user);
            }
        [Route("user/create")]
        // GET: Users/Create
        public IActionResult Create() {
            return View("~/Areas/Admin/Views/User/Create.cshtml");
            }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("user/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,email,password,id,isAdmin=false")] ManagementApp.Models.User user) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
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
        [Route("user/edit/{id}")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
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
            return View("~/Areas/Admin/Views/User/Edit.cshtml",user);
            }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("user/edit/{id}")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("id,name,email,password,isAdmin")] ManagementApp.Models.User user) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            var useritem = uow.Users.Get(id);

            user.id = id;

            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
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
                    useritem.email = user.email;
                    useritem.name = user.name;
                    useritem.password = user.password;
                    useritem.isAdmin = user.isAdmin;
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
            return View("~/Areas/Admin/Views/User/Edit.cshtml",user);
            }
        [Route("user/delete/{id}")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
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

            return View("~/Areas/Admin/Views/User/Delete.cshtml",user);
            }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("user/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin == false))
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
        
        }
    }
