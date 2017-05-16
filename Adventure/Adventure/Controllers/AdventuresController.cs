using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Adventure.DbContext;
using Adventure.Entities.Models;
using Adventure.DTOs;
using Microsoft.AspNet.Identity;

namespace Adventure.Controllers
{
    [Authorize]
    public class AdventuresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Adventures
        public ActionResult Index()
        {
            var adventures = db.Adeventures.Include(a => a.EventTopic).Include(a => a.Performer);
            return View(adventures.ToList());
        }

        // GET: Adventures/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adventures adventures = db.Adeventures.Find(id);
            if (adventures == null)
            {
                return HttpNotFound();
            }
            return View(adventures);
        }

        // GET: Adventures/Create
        public ActionResult Create()
        {
            ViewBag.EventTopicId = new SelectList(db.EventTopics, "Id", "Name");
            ViewBag.PerformerId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Adventures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdventuresEntry entry)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.Identity.GetUserId();


                Adventures adventures = new Adventures(UserId,entry.EventTopicId,entry.Title,entry.Description, entry.ExternalUrl,
                    entry.ImgUrl,
                    entry.GetDate(),
                    entry.VenueId,
                    DateTime.Now,
                    null);

                db.Adeventures.Add(adventures);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTopicId = new SelectList(db.EventTopics, "Id", "Name", entry.EventTopicId);
           // ViewBag.PerformerId = new SelectList(db.Users, "Id", "Email", entry.PerformerId);
            return View(entry);
        }

        // GET: Adventures/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adventures adventures = db.Adeventures.Find(id);
            if (adventures == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTopicId = new SelectList(db.EventTopics, "Id", "Name", adventures.EventTopicId);
            ViewBag.PerformerId = new SelectList(db.Users, "Id", "Email", adventures.PerformerId);
            return View(adventures);
        }

        // POST: Adventures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ExternalUrl,ImgUrl,EventDate,PerformerId,VenueId,EventTopicId,DateCreated,DateModified")] Adventures adventures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adventures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTopicId = new SelectList(db.EventTopics, "Id", "Name", adventures.EventTopicId);
            ViewBag.PerformerId = new SelectList(db.Users, "Id", "Email", adventures.PerformerId);
            return View(adventures);
        }

        // GET: Adventures/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adventures adventures = db.Adeventures.Find(id);
            if (adventures == null)
            {
                return HttpNotFound();
            }
            return View(adventures);
        }

        // POST: Adventures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Adventures adventures = db.Adeventures.Find(id);
            db.Adeventures.Remove(adventures);
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
