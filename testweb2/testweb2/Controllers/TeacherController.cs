using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testweb2.Func;
using testweb2.Models;

namespace testweb2.Controllers
{
    public class TeacherController : Controller
    {
        private CategoryDBcontext db = new CategoryDBcontext();
        private UserCategoriesDBcontext db2 = new UserCategoriesDBcontext();

        // GET: Teacher
        public ActionResult AddCat()
        {
            try
            {
                if (!Authen.Certification(Session["UserClass"].ToString(), Authen.UserClass.Teacher))
                {
                    return View("ClassError");
                }

                return View(db.Category.ToList());
            }
            catch
            {
                return View("ClassError");
            }
        }

        [HttpPost]
        public ActionResult AddCat([Bind(Include ="CatId,CatName,CatAttribute")]Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
                return View(db.Category.ToList());
            }
            return View(db.Category.ToList());
        }

        public ActionResult DeleteCat(int id)
        {
            var item = from a in db.Category.ToList()
                       where a.CatId == id
                       select a;
            var item2 = from a in db2.Categories.ToList()
                        where a.CatUSelect == id
                        select a;
            foreach(var a in item2)
            {
                db2.Categories.Remove(a);
                db2.SaveChanges();
            }
            db.Category.Remove(item.First());
            db.SaveChanges();
            return Redirect("~/Teacher/AddCat");
        }

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
