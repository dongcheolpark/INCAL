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

namespace testweb2.Controllers
{
    public class HomeworkController : Controller
    {
        private HomeworkDBContext db = new HomeworkDBContext();
        private CategoryDBcontext db2 = new CategoryDBcontext();
        private NoteCatDBContext db3 = new NoteCatDBContext();

        // GET: Homework
        public ActionResult Index()
        {
            var list = from a in db.Homework.ToList()
                       orderby a.Date descending
                       select a;
            return View(list);
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
            return View(homework);
        }

        // GET: Homework/Create
        public ActionResult Create()
        {
            try
            {
                if (!Authen.Certification(Session["UserClass"].ToString(), Authen.UserClass.Teacher))
                {
                    return View("ClassError");
                }
                
                return View(db2.Category.ToList());
            }
            catch
            {
                return View("ClassError");
            }
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "NoteNo,Subject,T_Name,Contents,Title,Date")] Homework homework,string[] checkbox)
        {
            if (ModelState.IsValid)
            {
                homework.T_Name = Session["UserName"].ToString();
                db.Homework.Add(homework);
                db.SaveChanges();

                for (int i = 0; i < checkbox.Length; i++)
                {
                    db3.NoteCat.Add(new NoteCat() {NoteCatId = 1, NoteNo = homework.NoteNo, CatAttribute = checkbox[i] });
                }

                db3.SaveChanges();

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("110.10.38.94"), 1503);
                client.Connect(iPEndPoint);
                return RedirectToAction("Index");
            }

            return View(homework);
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
    }
}
