using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ManagementApp.Models;

namespace ManagementApp.Controllers
{
    [Route("product")]
    public class ProductController : Controller
    {
        private IWebHostEnvironment he;

        public ProductController(IWebHostEnvironment e)
        {
            this.he = e;
        }

        [Route("index")]
        [Route("")]
        [Route("~/")]

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Posts post)
        {
            return View(post);
        }
        [Route("upload_ckeditor")]
        [HttpPost]
        public IActionResult UploadCKEditor(IFormFile upload)
        {
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + upload.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), he.WebRootPath, "uploads", fileName);
            var stream = new FileStream(path, FileMode.Create);
            upload.CopyTo(stream);
            return new JsonResult(new
            {
                uploaded = 1,
                fileName = upload.FileName,
                url = "/uploads/" + fileName
            });
        }

        [Route("filebrowse")]
        [HttpGet]
        public IActionResult FileBrowse()
        {
            var dir = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), he.WebRootPath, "uploads"));
            ViewBag.fileInfos = dir.GetFiles();
            return View("FileBrowse");
        }
    }
}
