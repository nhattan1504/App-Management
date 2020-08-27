using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ManagementApp.Models;
using ManagementApp.WorkOfUnit;
using cloudscribe.Pagination.Models;

namespace ManagementApp.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        UnitOfWork uow;
        public HomeController(ILogger<HomeController> logger, IServiceProvider provider) {
            _logger = logger;
            uow = new UnitOfWork(provider);
            }


        public IActionResult IndexPost(int pageSize = 3, int pageNumber = 3) {
            int ExcludeRecords = (pageSize * pageNumber) - pageSize;
            var postUser = uow.Post.Getpage().Where(p => p.isAccept == true).ToList()
                            .Skip(ExcludeRecords).Take(pageSize);
            var test = (double)uow.Post.Getpage().Count() / pageSize;
            var totalItem = (int)Math.Ceiling(test);
            var result = new PagedResult<Posts> {
                Data = postUser.ToList(),
                TotalItems = (long)totalItem,
                PageNumber = pageNumber,
                PageSize = pageSize
                };
            return View(result);

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
