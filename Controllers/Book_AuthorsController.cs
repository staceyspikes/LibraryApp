using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryApp.Models;

namespace LibraryApp.Controllers
{
    public class Book_AuthorsController : Controller
    {
        private LibraryAppEntities db = new LibraryAppEntities();

        // GET: Book_Authors
        public ActionResult Index()
        {
            var book_Authors = db.Book_Authors.Include(b => b.Author).Include(b => b.Book);
            return View(book_Authors.ToList());
        }

        // GET: Book_Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Authors book_Authors = db.Book_Authors.Find(id);
            if (book_Authors == null)
            {
                return HttpNotFound();
            }
            return View(book_Authors);
        }

        // GET: Book_Authors/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName");
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            return View();
        }

        // POST: Book_Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Books_AuthorID,AuthorID,BookID")] Book_Authors book_Authors)
        {
            if (ModelState.IsValid)
            {
                db.Book_Authors.Add(book_Authors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", book_Authors.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", book_Authors.BookID);
            return View(book_Authors);
        }

        // GET: Book_Authors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Authors book_Authors = db.Book_Authors.Find(id);
            if (book_Authors == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", book_Authors.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", book_Authors.BookID);
            return View(book_Authors);
        }

        // POST: Book_Authors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Books_AuthorID,AuthorID,BookID")] Book_Authors book_Authors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book_Authors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", book_Authors.AuthorID);
            ViewBag.BookID = new SelectList(db.Books, "BookID", "Title", book_Authors.BookID);
            return View(book_Authors);
        }

        // GET: Book_Authors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Authors book_Authors = db.Book_Authors.Find(id);
            if (book_Authors == null)
            {
                return HttpNotFound();
            }
            return View(book_Authors);
        }

        // POST: Book_Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_Authors book_Authors = db.Book_Authors.Find(id);
            db.Book_Authors.Remove(book_Authors);
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
