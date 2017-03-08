using AutoMapper;
using System.Net;
using System.Web.Mvc;
using Uspa.Admin.ViewModel;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Admin.Controllers
{
    public class BannersController : Controller
    {
        private IBanners bannersHandler = new BannersRepository();
        private ISites sitesHandler = new SitesRepository();

        public ActionResult Index()
        {
            return View(bannersHandler.All());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Banners banners = bannersHandler.GetById(id);

            if (banners == null)
            {
                return HttpNotFound();
            }
            return View(banners);
        }

        public ActionResult Create()
        {
            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,url,state,site_id")] BannersViewModel banner)
        {
            if (ModelState.IsValid)
            {

                Mapper.Initialize(cfg => cfg.CreateMap<BannersViewModel, Banners>());

                Banners banners = Mapper.Map<BannersViewModel, Banners>(banner);

                bannersHandler.Add(banners);
                return RedirectToAction("Index");
            }

            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title", banner.site_id);
            return View(banner);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mapper.Initialize(cfg => cfg.CreateMap<Banners, BannersViewModel>());
            BannersViewModel banner = Mapper.Map<Banners, BannersViewModel>(bannersHandler.GetById(id));


            if (banner == null)
            {
                return HttpNotFound();
            }

            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title", banner.site_id);
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,url,state,site_id")] BannersViewModel banner)
        {
            if (ModelState.IsValid)
            {

                Mapper.Initialize(cfg => cfg.CreateMap<BannersViewModel, Banners>());

                Banners banners = Mapper.Map<BannersViewModel, Banners>(banner);


                bannersHandler.Update(banners);
                return RedirectToAction("Index");
            }

            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title", banner.site_id);
            return View(banner);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banners banner = bannersHandler.GetById(id);

            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banners banners = bannersHandler.GetById(id);
            bannersHandler.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                sitesHandler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
