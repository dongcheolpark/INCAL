using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testweb2.Models;

namespace testweb2.Controllers
{
    public class studentController : Controller
    {
        private UserCategoriesDBcontext db = new UserCategoriesDBcontext();
        private CategoryDBcontext db2 = new CategoryDBcontext();

        // GET: student
        public ActionResult Settings()
        {
            return View();
        }
        public ActionResult ChangeCat()
        {
            StudentsModel model = new StudentsModel()
            {
                category = db2.Category.ToList(),
                userCategory = from a in db.Categories.ToList()
                               where a.CatUName == int.Parse(Session["UserNo"].ToString())
                               select a
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeCat(string[] checkbox)
        {
            var ab = from a in db.Categories.ToList()
                     where a.CatUName == int.Parse(Session["UserNo"].ToString())
                     select a;
            foreach(var item in ab)
            {
                db.Categories.Remove(item);
            }
            db.SaveChanges();
            SelectedCategory abc = new SelectedCategory()
            {
                CatUName = int.Parse(Session["UserNo"].ToString()),
                
            };
            for(int i = 0; i<checkbox.Length;i++)
            {
                abc.CatUSelect = int.Parse(checkbox[i]);
                db.Categories.Add(abc);
                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("Settings");
        }
        
        /*[HttpPost]
        public ActionResult ChangeCat()
        {
            return View();
        }*/

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
