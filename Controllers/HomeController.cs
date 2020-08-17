using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ManagementApp.Models;
using ManagementApp.WorkOfUnit;

namespace ManagementApp.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        UnitOfWork uow;
        public HomeController(ILogger<HomeController> logger, IServiceProvider provider) {
            _logger = logger;
            uow = new UnitOfWork(provider);
            }

        public IActionResult IndexPost() {
            var postUser = uow.Post.GetAll().Where(p => p.isAccept == true).ToList();
            return View(postUser);
            //return View();
            }

        public IActionResult Privacy() {
            return View();
            }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
