using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using auth_test.Models;

namespace auth_test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("/faq")]
        public IActionResult FAQ()
        {
            return View();
        }

        [Route("/eventadmin")]
        [Authorize]
        public IActionResult EventAdmin()
        {
            return View(new UserViewModel { Name = User.Identity.Name });
        }

        [Route("/agent")]
        [Authorize]
        public IActionResult Agent()
        {
            return View( new UserViewModel { Name = User.Identity.Name });
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
