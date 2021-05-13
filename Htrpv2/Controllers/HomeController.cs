using Htrpv2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Htrpv2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db, ILogger<HomeController> logger)
        {
            this._db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.CountItems = _db.Items.Count();
            return View(ViewBag.CountItems);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "User")]


        public IActionResult List()
        {

            var userName = User.Identity.Name;
            List<Item> RightItems = _db.Items.Where(n => n.ApplicationUser == _db.Users.Where(a => a.Email == userName).FirstOrDefault()).ToList();

            return View(RightItems);
        }
        [Authorize(Roles = "User")]
        public IActionResult ListFood()
        {

            var userName = User.Identity.Name;
            List<Item> RightItems = _db.Items.Where(n => n.ApplicationUser == _db.Users.Where(a => a.Email == userName).FirstOrDefault()).ToList();

            return View(RightItems.Where(f => f.IsItFood == true).ToList());
            //List<Item> Items = _db.Items.Where(f => f.IsItFood == true).ToList();
            //return View(Items);
        }

    }
}
