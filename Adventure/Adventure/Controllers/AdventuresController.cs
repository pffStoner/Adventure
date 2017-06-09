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
using Adventure.Entities.Common;
using Adventure.Entities;

namespace Adventure.Controllers
{
  
    public class AdventuresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Adventures
        [Authorize]
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var currentUserId = User.Identity.GetUserId();
            
            // var currentUser = db.Adeventures.Include(u => u.Performer);
            var userRole = User.IsInRole("Administrator");
            var adventures1 = db.Adeventures.Include(a => a.EventTopic).Include(a => a.Performer);
            if (userRole)
            {

                var adventures = from s in db.Adeventures
                                 select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    adventures = adventures.Where(s => s.Title.Contains(searchString)
                                           || s.Description.Contains(searchString) || s.VenueId.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        adventures = adventures.OrderByDescending(s => s.Title);
                        break;
                    case "date_desc":
                        adventures = adventures.OrderByDescending(s => s.PerformerId);
                        break;
                    default:
                        adventures = adventures.OrderBy(s => s.Title);
                        break;
                }

                return View(adventures.ToList());
            }
            else
            {
                var adventures = from s in db.Adeventures
                                 select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    adventures = adventures.Where(s => s.Title.Contains(searchString)
                                           || s.Description.Contains(searchString) || s.VenueId.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        adventures = adventures.OrderByDescending(s => s.Title);
                        break;
                    //case "Date":
                    //    adventures = adventures.OrderBy(s => s.Performer);
                    //    break;
                    case "date_desc":
                        adventures = adventures.OrderByDescending(s => s.PerformerId);
                        break;
                    default:
                        adventures = adventures.OrderBy(s => s.Title);
                        break;
                }
                return View(adventures.ToList().Where(
                                   list => list.PerformerId == currentUserId));
            }

        }

        // GET: Adventures
        [Authorize]
        public ActionResult PublicIndex(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            // var currentUser = db.Adeventures.Include(u => u.Performer);
            //    var currentUserId = User.Identity.GetUserId();
            // var userRole = User.IsInRole("Administrator");
            //     var publicArticle = db.Adeventures.Where(p => p.EventTopicId == "public");
            //      var adventures1 = db.Adeventures.Include(a => a.EventTopic).Include(a => a.Performer);


            var adventures = from s in db.Adeventures
                             select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                adventures = adventures.Where(s => s.Title.Contains(searchString)
                                       || s.Description.Contains(searchString) || s.VenueId.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    adventures = adventures.OrderByDescending(s => s.Title);
                    break;
                case "date_desc":
                    adventures = adventures.OrderByDescending(s => s.PerformerId);
                    break;
                default:
                    adventures = adventures.OrderBy(s => s.Title);
                    break;
            }

            return View(adventures.Where(p => p.EventTopicId == "6cb05fe8-5bda-4f6b-85b0-a64568e5c8aa").ToList());



        }




        // GET: Adventures/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            Adventures adventures = db.Adeventures.Find(id);
            ViewBag.VideoUrl = adventures.ExternalUrl.ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
       
            if (adventures == null)
            {
                return HttpNotFound();
            }
            return View(adventures);
        }

        // GET: Adventures/Create
        [Authorize]
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
        [Authorize]
        public ActionResult Create(AdventuresEntry entry)
        {
            if (ModelState.IsValid)
            {
                // var UserId = User.Identity.GetUserId();


                Adventures adventures = new Adventures(User.Identity.GetUserId(), entry.EventTopicId,
                    entry.Title, entry.Description, entry.ExternalUrl,
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
        [Authorize]
        public ActionResult Edit(string id)
        {                        ModelState.Remove("PerformerId");

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
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ExternalUrl,ImgUrl,EventDate,VenueId,PerformerId,EventTopicId,DateCreated,DateModified")] Adventures adventures)
        {
            ModelState.Remove("PerformerId");

            if (ModelState.IsValid)
            {
                ModelState.Remove("PerformerId");

                db.Entry(adventures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTopicId = new SelectList(db.EventTopics, "Id", "Name", adventures.EventTopicId);
            ViewBag.PerformerId = new SelectList(db.Users, "Id", "Email", adventures.PerformerId);
            return View(adventures);
        }

        // GET: Adventures/Delete/5
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(string id)
        {
            Adventures adventures = db.Adeventures.Find(id);
            db.Adeventures.Remove(adventures);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost, ActionName("Vote1")]
        // [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Vote1(Adventures adventures, string id)
        {
            ModelState.Remove("Id");
            ModelState.Remove("Title");
            ModelState.Remove("Description");
            ModelState.Remove("ExternalUrl");
            ModelState.Remove("ImgUrl");
            ModelState.Remove("PerformerId");
            ModelState.Remove("VenueId");
            ModelState.Remove("EventTopicId");


            var usr = User.Identity.GetUserId();
            User user = db.Users.Find(usr);

            if (ModelState.IsValid && user.IsVoted == 0)
            {
                adventures = db.Adeventures.Find(id);
                adventures.VoteCount++;
                user.IsVoted = 1;


                var errors = ModelState.Values.SelectMany(v => v.Errors);
                db.Entry(adventures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        // GET: 
        [Authorize]
        public ActionResult Vote1(string id)
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
            return RedirectToAction("Index");
        }
        public ActionResult Top10(string sortOrder)
        {
            //      var top10 = db.Adeventures.OrderByDescending(adv => adv.VoteCount);

            
                //string userName = Session["UserName"].ToString();

                // Convert sort order
              //  ViewBag.VoteCountSort = sortOrder == "VoteCount" ? "VoteCount_desc" : "VoteCount";

                var model = from t in db.Adeventures
                            where t.VoteCount > 0
                            where t.EventTopicId== "6cb05fe8-5bda-4f6b-85b0-a64568e5c8aa"
                            select t;

           
                model = model.OrderByDescending(t => t.VoteCount).Take(10);
            
                       
            
                return View(model.ToList());
            }


            //if (ModelState.IsValid)
            //{
            //    // var UserId = User.Identity.GetUserId();


            //    Adventures adventures = new Adventures(User.Identity.GetUserId(), entry.EventTopicId,
            //        entry.Title, entry.Description, entry.ExternalUrl,
            //        entry.ImgUrl,
            //        entry.GetDate(),
            //        entry.VenueId,
            //        DateTime.Now,
            //        null);

            //    db.Adeventures.Add(adventures);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");

            //var query =
            //    from adv in db.Adeventures
            //    where adv.Id == id\
            //    select adv;


            //foreach (Adventures adv in query)
            //{
            //    adv.VoteCount++;
            //    // Insert any additional changes to column values.
            //}



        }
    }

