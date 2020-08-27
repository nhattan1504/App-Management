using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementApp.Models;
using AppContext = ManagementApp.Models.AppContext;
using ManagementApp.WorkOfUnit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace ManagementApp.Controllers {
    public class PostsController : Controller {
        UnitOfWork uow;
        //private readonly AppContext _context;

        public PostsController(IServiceProvider provider) {
            uow = new UnitOfWork(provider);
            }

        //[Microsoft.AspNetCore.Authorization.AllowAnonymous]
        // GET: Posts
        //public async Task<IActionResult> Index() {
        //    if (HttpContext.Session.GetString("username") == null)
        //        {
        //        return NotFound();
        //        }
        //    return View(uow.Post.GetAll());
        //    }
        ////Get: PostOfUser
        //public async Task<IActionResult> IndexUser() {
        //    var postUser = uow.Post.GetAll().Where(p => p.isAccept == true).ToList();
        //    return View(postUser);
        //    }
        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int id) {
            if (id == null)
                {
                return NotFound();
                }
            var posts = uow.Post.Get(id);
            if (posts == null)
                {
                return NotFound();
                }

            return View(posts);
            }

        // GET: Posts/Create
        //public IActionResult Create() {
        //    return View();
        //    }

        //// POST: Posts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,content,title,isAccept=false")] Posts posts) {
        //    //if (HttpContext.Session.GetString("username") == null)
        //    //    {
        //    //    return NotFound();
        //    //    }
        //    if (ModelState.IsValid)
        //        {
        //        uow.Post.Add(posts);
        //        return Redirect("Create");
        //        }
        //    return View(posts);
        //    }

        //// GET: Posts/Edit/5
        //public async Task<IActionResult> Edit(int id) {
        //    if (HttpContext.Session.GetString("username") == null)
        //        {
        //        return NotFound();
        //        }
        //    if (id == null)
        //        {
        //        return NotFound();
        //        }

        //    var posts = uow.Post.Get(id);
        //    if (posts == null)
        //        {
        //        return NotFound();
        //        }
        //    return View(posts);
        //    }

        //// POST: Posts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("id,content,title,isAccept")] Posts posts) {
        //    if (HttpContext.Session.GetString("username") == null)
        //        {
        //        return NotFound();
        //        }
        //    if (id != posts.id)
        //        {
        //        return NotFound();
        //        }

        //    if (ModelState.IsValid)
        //        {
        //        try
        //            {
        //            uow.Post.Update(posts);
        //            }
        //        catch (DbUpdateConcurrencyException)
        //            {
        //            //if (!PostsExists(posts.id))
        //            //{
        //            //    return NotFound();
        //            //}
        //            //else
        //            //{
        //            //    throw;
        //            //}
        //            }
        //        return RedirectToAction(nameof(Index));
        //        }
        //    return View(posts);
        //    }

        //// GET: Posts/Delete/5
        //public async Task<IActionResult> Delete(int id) {
        //    if (HttpContext.Session.GetString("username") == null)
        //        {
        //        return NotFound();
        //        }
        //    if (id == null)
        //        {
        //        return NotFound();
        //        }

        //    var posts = uow.Post.Get(id);
        //    if (posts == null)
        //        {
        //        return NotFound();
        //        }

        //    return View(posts);
        //    }

        //// POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id) {
        //    if (HttpContext.Session.GetString("username") == null)
        //        {
        //        return NotFound();
        //        }
        //    var posts = uow.Post.Get(id);
        //    uow.Post.Remove(posts);
        //    return RedirectToAction(nameof(Index));
        //    }

        //private bool PostsExists(int id)
        //{
        //    return _context.Postss.Any(e => e.id == id);
        //}
        }
    }
