using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApp.Areas.Admin {
    [Authorize]
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
            }
        }
    }
