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
        private UserCategoryDBcontext db = new UserCategoryDBcontext();
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
                userCategory = from a in db.UserCategory.ToList()
                               where a.CatUId == int.Parse(Session["UserId"].ToString())
                               select a
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeCat(string[] checkbox)
        {
            UserCategories abc = new UserCategories()
            {
                CatUName = int.Parse(Session["UserId"].ToString()),
                
            };
            for(int i = 0; i<checkbox.Length;i++)
            {
                abc.CatUSelect = int.Parse(checkbox[i]);
                db.UserCategory.Add(abc);
            }
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
