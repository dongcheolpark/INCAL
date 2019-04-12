using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testweb2;
using testweb2.Models;

namespace testweb2.Controllers
{
    public class UsersController : Controller
    {
        private UserDBContext db = new UserDBContext();

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserNo,UserId,UserPassword,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                var id = from a in db.Users.ToList()
                         orderby a.UserNo
                         select a;
                user.UserNo = id.Last().UserNo+2;
                user.UserClass = "students";
                user.UserPassword = Encryption.Encode(user.UserPassword);
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("index", "home");
            }

            return View();
        }

        public ActionResult Login()
        {
            if(Session["UserName"] != null)
            {
                return Redirect("~/home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserId,UserPassword")]User user, string btnSubmit)
        {
            switch (btnSubmit)
            {
                case "로그인":

                    User result = null;
                    if (ModelState.IsValid)
                    {
                        var pw = user.UserPassword;
                        pw = Encryption.Encode(pw);
                        var iscorrect = from a in db.Users.ToList()
                                        where a.UserId == user.UserId
                                        where a.UserPassword == pw
                                        select a;
                        foreach (var i in iscorrect)
                        {
                            result = i;
                        }
                        if (result == null)
                        {
                            return View();
                        }


                    }
                    Session["UserClass"] = result.UserClass;
                    Session["UserName"] = result.UserName;
                    Session["UserId"] = result.UserId;
                    Session["UserPw"] = result.UserPassword;
                    return Redirect("~/home");

                case "로그아웃":
                    Session["UserClass"] = null;
                    Session["UserName"] = null;
                    Session["UserId"] = null;
                    Session["UserPw"] = null;
                    return Redirect("~/home");

                default:
                    return View();
            }
        }

        public ActionResult Error()
        {
            return View();
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
