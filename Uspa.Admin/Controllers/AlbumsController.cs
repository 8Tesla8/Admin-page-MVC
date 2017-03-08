using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Uspa.Admin.ViewModel;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;
using Uspa.Domain.Settings;

namespace Uspa.Admin.Controllers
{
    public class AlbumsController : Controller
    {
        public AlbumsController()
        {

        }

        private IAlbum albumHandler = new AlbumRepository();
        private ILanguages languageHandler = new LanguagesRepository();


        // GET: Albums
        public ActionResult Index(int? page, int? language, string search)
        {
            var albums = albumHandler.All();
            // Filter: 
            if (language != null && language != 0)
                albums = albums.Where(m => m.language_id == language);
            // Search:
            albums = albumHandler.Search(albums, search);

            List<Languages> languageList = languageHandler.All().ToList();
            languageList.Insert(0, new Languages { id = 0, title = "All" });
            ViewBag.Language = new SelectList(languageList, "id", "title");

            int pageSize = PagingSettings.PageSizeInAlbum;
            int pageNumber = (page ?? 1);
            return View(albums.ToPagedList(pageNumber, pageSize));
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Albums album = albumHandler.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }


        public ActionResult Create()
        {
            UspaDbEntities db = new UspaDbEntities();
            ViewBag.language_id = new SelectList(languageHandler.All().
                  Select(c => new { id = c.id, title = c.title }), "id", "title");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,created")] AlbumViewModel albumModel, int language_id)
        {
            ViewBag.language_id = new SelectList(languageHandler.All().
                 Select(c => new { id = c.id, title = c.title }), "id", "title");

            if (ModelState.IsValid)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<AlbumViewModel, Albums>());
                DateTime date = DateTime.Now;
                albumModel.created = date;
                albumModel.language_id = language_id;
                Albums album = Mapper.Map<AlbumViewModel, Albums>(albumModel);
                albumHandler.Add(album);
                return RedirectToAction("Index");
            }


            return View(albumModel);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mapper.Initialize(cfg => cfg.CreateMap<Albums, AlbumViewModel>());
            AlbumViewModel albumModel = Mapper.Map<Albums, AlbumViewModel>(albumHandler.GetById(id));
            if (albumModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.language_id = new SelectList(languageHandler.All().
                  Select(c => new { id = c.id, title = c.title }), "id", "title");
            ViewBag.language = languageHandler.All();
            return View(albumModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,previd,created,modified")] AlbumViewModel albumModel, int? language_id)
        {

            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                albumModel.modified = date;
                if (language_id != null)
                {
                    albumModel.language_id = language_id;
                }
                Mapper.Initialize(cfg => cfg.CreateMap<AlbumViewModel, Albums>());
                Albums album = Mapper.Map<AlbumViewModel, Albums>(albumModel);
                albumHandler.Update(album);

                return RedirectToAction("Index");
            }
            ViewBag.language_id = new SelectList(languageHandler.All().
                Select(c => new { id = c.id, title = c.title }), "id", "title");
            ViewBag.language = languageHandler.All();
            return View(albumModel);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Albums album = albumHandler.GetById(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Albums album = albumHandler.GetById(id);
            albumHandler.Delete(album);
            return RedirectToAction("Index");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                albumHandler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
