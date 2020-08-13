using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;
using testweb2.Models;
using testweb2.Func;
using testweb2.Classes;

namespace testweb2.Controllers
{
    public class HomeworkController : Controller
    {
        private HomeworkDBContext db = new HomeworkDBContext();
        private CategoryDBcontext db2 = new CategoryDBcontext();
        private NoteCatDBContext db3 = new NoteCatDBContext();
        private UserCategoriesDBcontext db4 = new UserCategoriesDBcontext();

        // GET: Homework
        public ActionResult Index()
        {
            DelHomeworks();
            var userlist = from a in db4.Categories.ToList()
                           where a.CatUName == int.Parse(Session["UserNo"].ToString())
                           select a;
            var list = from a in db.Homework.ToList()
                       orderby a.Date descending
                       select a;
            List<Homework> result = new List<Homework>();
            foreach(var item in list)
            {
                bool check = false;
                var catlist = from a in db3.NoteCat.ToList()
                              where a.NoteNo == item.NoteNo
                              select a;
                foreach (var item2 in userlist)
                {
                    foreach(var item3 in catlist)
                    {
                        if (int.Parse(item3.CatAttribute) == item2.CatUSelect) {
                            check = true;
                            break;
                        }
                    }
                    if (check) break;
                }
                if(check)
                {
                    result.Add(item);
                }
            }
            return View(result);
            //return View(db.Homework.ToList());
        }

        // GET: Homework/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homework.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            var a = from b in db3.NoteCat.ToList()
                    where b.NoteNo == id
                    select b;
            List<string> vs = new List<string>();
            foreach(var item in a)
            {
                foreach(var item2 in db2.Category.ToList()) { 
                    if(int.Parse(item.CatAttribute) == item2.CatId)
                    {
                        vs.Add(item2.CatName);
                    }
                }
            }
            var c = new HomeworkDetail(homework, a, vs);
            return View(c);
        }

        // GET: Homework/Create
        public ActionResult Create()
        {
            try
            {
                if (!Authen.Certification(Session["UserClass"].ToString(), Authen.UserClass.Teacher))
                {
                    return RedirectToAction("PermitionEr", "Error");

                }
                
                return View(db2.Category.ToList());
            }
            catch
            {
                return RedirectToAction("LoginEr", "Error");
            }
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "NoteNo,Subject,T_Name,Contents,Title,Date")] Homework homework,string[] checkbox)
        {
            try
            {
                if (!Authen.Certification(Session["UserClass"].ToString(), Authen.UserClass.Teacher))
                {
                    return RedirectToAction("PermitionEr", "Error");

                }

                if (ModelState.IsValid)
                {
                    homework.T_Name = Session["UserName"].ToString();
                    db.Homework.Add(homework);
                    db.SaveChanges();

                    for (int i = 0; i < checkbox.Length; i++)
                    {
                        db3.NoteCat.Add(new NoteCat() { NoteNo = homework.NoteNo, CatAttribute = checkbox[i] });
                    }

                    db3.SaveChanges();

                    /*Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("110.10.38.94"), 1503);
                    client.Connect(iPEndPoint);
                    return RedirectToAction("Index");*/
                }

                return Redirect("~/homework");
            }
            catch
            {
                return RedirectToAction("LoginEr", "Error");
            }
        }

        // GET: Homework/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homework.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoteNo,Subject,T_Name,Contents,Title,Date")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homework).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homework);
        }

        // GET: Homework/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homework.Find(id);

            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        [HttpPost]
        public string Image(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    file.SaveAs(path);
                    return string.Format(@"http://incal.iptime.org/images/" + fileName);
                }
            }
            return null;
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homework homework = db.Homework.Find(id);
            CommentDBContext cdb = new CommentDBContext();
            var comments = from a in cdb.Comment.ToList()
                           where a.ParentNo == id
                           select a;
            foreach (var item in comments)
            {
                cdb.Comment.Remove(item);
            }
            db.Homework.Remove(homework);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        void DelHomeworks()
        {
            var data = from a in db.Homework.ToList()
                            where a.Date < DateTime.Today
                            select a;
            foreach(var item in data)
            {
                db.Homework.Remove(item);
            }
            db.SaveChanges();
            return;
        }
    }
}
