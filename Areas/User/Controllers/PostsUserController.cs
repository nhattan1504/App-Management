﻿using System;
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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ManagementApp.Areas.User.Controllers
{
    [Area("User")]
    [Route("user")]
    public class PostsUserController : Controller {
            
        UnitOfWork uow;
        private readonly IWebHostEnvironment _hostEnvirontment;

        //private readonly AppContext _context;

        public PostsUserController(IServiceProvider provider,IWebHostEnvironment hostEnvironment) {
            uow = new UnitOfWork(provider);
            this._hostEnvirontment = hostEnvironment;
            }

        //[Microsoft.AspNetCore.Authorization.AllowAnonymous]
        // GET: Posts
        [Route("index")]
        public async Task<IActionResult> Index() {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            var postUser = uow.Post.GetAll().Where(p => p.Userid == userLogined.id).ToList();
            return View("~/Areas/User/Views/Index.cshtml",postUser);
            }
        //Get: PostOfUser
        public async Task<IActionResult> IndexUser() {
            //var postUser = uow.Post.GetAll().Where(p => p.isAccept == true).ToList();
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            var postUser = uow.Post.GetAll().Where(p => p.User.id == userLogined.id).ToList();
            return View(postUser);
            }
        // GET: Posts/Details/5
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            if (id == null)
                {
                return NotFound();
                }


            var posts = uow.Post.Get(id);
            if (posts == null)
                {
                return NotFound();
                }

            return View("~/Areas/User/Views/Details.cshtml",posts);
            }

        // GET: Posts/Create
        [Route("create")]
        public IActionResult Create() {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin)) {
                return NotFound();
                }
                return View("~/Areas/User/Views/Create.cshtml");
            }
        

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,content,title,ImageFile,description,isAccept=false")] Posts posts) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            if (HttpContext.Session.GetString("username") == null)
                    {
                    return NotFound();
                    }
                if (posts.content == null || posts.description == null || posts.title == null)
                    {
                    TempData["Message"] = "Cant input space in element when create";
                    return View("~/Areas/User/Views/Create.cshtml");
                    }
                else
                    {
                if (ModelState.IsValid)
                        {
                        string wwwRootPath = _hostEnvirontment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(posts.ImageFile.FileName);
                        string extension = Path.GetExtension(posts.ImageFile.FileName);
                        posts.imageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                            await posts.ImageFile.CopyToAsync(fileStream);
                            }
                    posts.Userid = userLogined.id;
                    uow.Post.Add(posts);
                        return Redirect("Create");
                        }
                    return View(posts);
                    }
                }
            
                
            
            // GET: Posts/Edit/5
            [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            if (id == null)
                {
                return NotFound();
                }

            var posts = uow.Post.Get(id);
            if (posts == null)
                {
                return NotFound();
                }
            return View("~/Areas/User/Views/Edit.cshtml",posts);
            }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,content,title,imageUrl,description,isAccept=false")] Posts posts) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            if (id != posts.id)
                {
                return NotFound();
                }
            if (ModelState.IsValid)
                {
                //string wwwRootPath = _hostEnvirontment.WebRootPath;
                //string fileName = Path.GetFileNameWithoutExtension(posts.ImageFile.FileName);
                //string extension = Path.GetExtension(posts.ImageFile.FileName);
                //posts.imageUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                //string path = Path.Combine(wwwRootPath + "/image/", fileName);
                //using (var fileStream = new FileStream(path, FileMode.Create))
                //    {
                //    await posts.ImageFile.CopyToAsync(fileStream);
                //    }
                posts.Userid = userLogined.id;
                    uow.Post.Update(posts);
                    
                return RedirectToAction(nameof(Index));
                }
            return View("~/Areas/User/Views/Edit.cshtml",posts);
            }

        // GET: Posts/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            if (id == null)
                {
                return NotFound();
                }

            var posts = uow.Post.Get(id);
            if (posts == null)
                {
                return NotFound();
                }

            return View("~/Areas/User/Views/Delete.cshtml",posts);
            }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
            if ((HttpContext.Session.GetString("username") == null) || (userLogined.isAdmin))
                {
                return NotFound();
                }
            var posts = uow.Post.Get(id);
            uow.Post.Remove(posts);
            return RedirectToAction(nameof(Index));
            }
        //private bool PostsExists(int id)
        //{
        //    return _context.Postss.Any(e => e.id == id);
        //}
        }
    }

