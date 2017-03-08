
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;
using Uspa.Domain.Settings;

namespace Uspa.Admin.Controllers
{
    public class ImagesController : Controller
    {
        //private UspaDbEntities db = new UspaDbEntities();
        IImage imageHandler = new ImagesRepository();
        IAlbum albumHandler = new AlbumRepository();
        IUsers userHandler = new UsersRepository();
        // GET: Images
        public ActionResult Index(int? page, int? album, string search)
        {


            var images = imageHandler.All();

            // Filter: 
            if (album != null && album != 0)
                images = images.Where(im => im.Albums.id == album);

            // Search:
            images = imageHandler.Search(images, search);

            List<Albums> albumList = albumHandler.All().ToList();
            albumList.Insert(0, new Albums { id = 0, title = "All" });
            ViewBag.Albums = new SelectList(albumList, "id", "title");

            int pageSize = PagingSettings.PageSizeInAlbum;
            int pageNumber = (page ?? 1);
            return View(images.ToPagedList(pageNumber, pageSize));
        }




        // GET: Images/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = imageHandler.GetById(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            ViewBag.album_id = new SelectList(albumHandler.All(), "id", "title");
            ViewBag.createdByUser_id = new SelectList(userHandler.All(), "id", "name");
            return View();
        }

        // POST: Images/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,filePath,created,state,createdByUser_id,album_id")] Images images, HttpPostedFileBase file)
        {


            foreach (string upload in Request.Files)
            {
                file = Request.Files[upload];
                if (file != null)
                {
                    List<Albums> alb = albumHandler.All().ToList();
                    Albums a = alb.Where(x => x.id == images.album_id).FirstOrDefault();
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/ImagesUpload") + "/" + a.title);
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/ImagesUpload" + "/" + a.title), fileName);
                    file.SaveAs(path);
                    string tempUrl = a.title + "/" + fileName;
                    images.filePath = tempUrl;

                }

            }

            if (ModelState.IsValid && images.filePath != null)
            {
                images.created = DateTime.Now;
                imageHandler.Add(images);
                return RedirectToAction("Index");
            }

            ViewBag.album_id = new SelectList(albumHandler.All(), "id", "title", images.album_id);
            ViewBag.createdByUser_id = new SelectList(userHandler.All(), "id", "name", images.createdByUser_Id);
            return View(images);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = imageHandler.GetById(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            ViewBag.album_id = new SelectList(albumHandler.All(), "id", "title", images.album_id);
            ViewBag.createdByUser_id = new SelectList(userHandler.All(), "id", "name", images.createdByUser_Id);
            return View(images);
        }

        // POST: Images/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,filePath,created,state,createdByUser_id,album_id")] Images images)
        {
            List<Albums> alb = albumHandler.All().ToList();
            Albums a = alb.Where(x => x.id == images.album_id).FirstOrDefault();
            System.IO.Directory.CreateDirectory(Server.MapPath("~/ImagesUpload") + "/" + a.title);
            try
            {
                string path1 = Server.MapPath("~/ImagesUpload" + "/" + images.filePath);
                string path2 = Server.MapPath("~/ImagesUpload" + "/" + a.title);
                if (!path1.Equals(path2))
                {

                    path2 = Server.MapPath("~/ImagesUpload" + "/" + a.title + "/" + Path.GetFileName(path1));
                    string newFileName = Path.GetFileName(path1);
                    System.IO.File.Move(path1, path2);


                    images.filePath = a.title + "/" + newFileName;
                }

            }
            catch (Exception)
            {

            }

            if (ModelState.IsValid)
            {


                imageHandler.Update(images);


                return RedirectToAction("Index");
            }
            ViewBag.album_id = new SelectList(albumHandler.All(), "id", "title", images.album_id);
            ViewBag.createdByUser_id = new SelectList(userHandler.All(), "id", "name", images.createdByUser_Id);
            return View(images);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Images images = imageHandler.GetById(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            imageHandler.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                imageHandler.Dispose();
                userHandler.Dispose();
                albumHandler.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}
