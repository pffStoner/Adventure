using Adventure.DbContext;
using Adventure.Entities.Common;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Adventure.Controllers
{
    public class PhotoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }


        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {


            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }

        // GET: Photo
        public ActionResult Index(string id,string artigleId, string filter = null, int page = 1, int pageSize = 20)
        {         

          artigleId = db.Adeventures.Find(id).Id;
           
          
            Session["articleId"] = artigleId;

            //     ViewBag.ArticleId = artigleId;

            var records = new PagedList<Photo>();
            ViewBag.filter = filter;

            records.Content = db.Photo
                        .Where(x => filter == null || (x.Decription.Contains(filter))).Where(list=>list.ArticleId == artigleId)
                        .OrderByDescending(x => x.PhotoId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
            // Count
            //editetetet
            records.TotalRecords = db.Photo
                            .Where(x => filter == null  || (x.Decription.Contains(filter))).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;



            return View(records);
        }

        // GET: Photo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
        //    var artigleId = (string)Session["articleId"];
            //if (artigleId == null)
            //{
            //    return HttpNotFound();
            //}
            var artId= (string)Session["articleId"];
            var photo = new Photo();
            return View(photo);
        }
        [HttpPost]
        public ActionResult Create(Photo photo, IEnumerable<HttpPostedFileBase> files, string id)
        {
            if (!ModelState.IsValid)
                return View(photo);
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            var model = new Photo();
            foreach (var file in files)
            {
                if (file.ContentLength == 0) continue;

                model.Decription = photo.Decription;
                var fileName = Guid.NewGuid().ToString();
                var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

                using (var img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    model.ThumbPath = String.Format("/GalleryImages/thumbs/{0}{1}", fileName, extension);
                    model.ImagePath = String.Format("/GalleryImages/{0}{1}", fileName, extension);

                    // Save thumbnail size image, 100 x 100
                    SaveToFolder(img, fileName, extension, new Size(100, 100), model.ThumbPath);

                    // Save large size image, 800 x 800
                    SaveToFolder(img, fileName, extension, new Size(600, 600), model.ImagePath);
                }

                // Save record to database
                model.CreatedOn = DateTime.Now;
                model.PerformerId= User.Identity.GetUserId();
                model.ArticleId = (string)Session["articleId"];
                db.Photo.Add(model);
                db.SaveChanges();
            }

            return RedirectPermanent("/photo/index/"+model.ArticleId);
        }
        //// POST: Photo/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Photo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Photo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Photo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Photo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
