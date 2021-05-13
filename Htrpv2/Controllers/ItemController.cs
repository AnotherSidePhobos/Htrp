using Htrpv2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Htrpv2.Controllers
{
    public class ItemController : Controller
    {
        AppDbContext db;
        public ItemController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Item> Items = db.Items;
            return View(Items);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            var a = User.Identity.Name;
            var u = db.Users.Where(n => n.Email == a);
            item.ApplicationUser = u.FirstOrDefault();


            db.Items.Add(item);
            db.SaveChanges();
            return Redirect("/Home/List");
        }

        public IActionResult Delete(int? id)
        {
            DeleteMeth(id);
            return Redirect("/Home/List");
        }
        public void DeleteMeth(int? id)
        {
            var temp = db.Items.Find(id);
            db.Items.Remove(temp);
            db.SaveChanges();
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = db.Items.Find(id);
            if (obj != null)
            {
                return View(obj);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Update(item);
                db.SaveChanges();
                return Redirect("/Home/List");
            }
            return View(item);

        }
    }
}
