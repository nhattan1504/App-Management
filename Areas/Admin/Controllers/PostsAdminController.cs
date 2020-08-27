using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementApp.Models;
using ManagementApp.WorkOfUnit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ManagementApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class PostsAdminController : Controller 
     {
        UnitOfWork uow;
        private readonly IWebHostEnvironment _hostEnvirontment;
        //private readonly AppContext _context;

        public PostsAdminController(IServiceProvider provider, IWebHostEnvironment hostEnvironment) {
            uow = new UnitOfWork(provider);
            this._hostEnvirontment = hostEnvironment;
            }

        //[Microsoft.AspNetCore.Authorization.AllowAnonymous]
        // GET: Posts
        [Route("post")]
        public async Task<IActionResult> Index() {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            return View("~/Areas/Admin/Views/Post/Index.cshtml", uow.Post.GetAll());
            }
        //Get: PostOfUser
        public async Task<IActionResult> IndexUser() {
            var postUser = uow.Post.GetAll().Where(p => p.isAccept == true).ToList();
            return View(postUser);
            }
        // GET: Posts/Details/5
        [Route("post/details/{id}")]
        public async Task<IActionResult> Details(int id) {
            //if (HttpContext.Session.GetString("username") == null)
            //    {
            //    return NotFound();
            //    }
            if (id == null)
                {
                return NotFound();
                }


            var posts = uow.Post.Get(id);
            if (posts == null)
                {
                return NotFound();
                }

            return View("~/Areas/Admin/Views/Post/Details.cshtml",posts);
            }

        // GET: Posts/Create
        [Route("post/create")]
        public IActionResult Create() {
            return View("~/Areas/Admin/Views/Post/Create.cshtml");
            }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("post/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,content,title,ImageFile,description,isAccept")] Posts posts) {
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
                var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
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
        [Route("post/edit/{id}")]
        public async Task<IActionResult> Edit(int id) {
            if (HttpContext.Session.GetString("username") == null)
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
            return View("~/Areas/Admin/Views/Post/Edit.cshtml",posts);
            }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("post/edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,content,title,imageUrl,ImageFile,description,isAccept")] Posts posts) {
            if (HttpContext.Session.GetString("username") == null)
                {
                return NotFound();
                }
            if (id != posts.id)
                {
                return NotFound();
                }

            if (ModelState.IsValid)
                {
                try
                    {
                    var userLogined = uow.Users.GetAll().Where(p => p.name == HttpContext.Session.GetString("username")).FirstOrDefault();
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
                    uow.Post.Update(posts);
                    }
                catch (DbUpdateConcurrencyException)
                    {
                    }
                return RedirectToAction(nameof(Index));
                }
            return View("~/Areas/Admin/Views/Post/Edit.cshtml", posts);
            }

        // GET: Posts/Delete/5
        [Route("post/delete/{id}")]
        public async Task<IActionResult> Delete(int id) {
            if (HttpContext.Session.GetString("username") == null)
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

            return View("~/Areas/Admin/Views/Post/Delete.cshtml",posts);
            }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("post/delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (HttpContext.Session.GetString("username") == null)
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
