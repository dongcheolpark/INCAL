using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testweb2.Models;

namespace testweb2.Controllers
{
    public class AdminController : Controller
    {
        private UserDBContext db = new UserDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult upgrade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult upgrade(string UserId, string UserClass)
        {
            var result = from a in db.Users.ToList()
                         where a.UserId == UserId
                         select a;
            foreach(var item in result)
            {
                item.UserClass = UserClass;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Redirect("/home/");
        }
    }
}