using System.Net;
using System.Web.Mvc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Admin.Controllers
{
    public class SitesController : Controller
    {
        private ISites sitesHandler = new SitesRepository();
        private ILanguages languagesHandler = new LanguagesRepository();

        public ActionResult Index()
        {
            var sites = sitesHandler.All();
            return View(sites);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sites site = sitesHandler.GetById(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        public ActionResult Create()
        {
            ViewBag.language_id = new SelectList(languagesHandler.All(), "id", "title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,countContent,language_id")] Sites site)
        {
            if (ModelState.IsValid)
            {
                if (site.countContent >= 0)
                {
                    sitesHandler.Add(site);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.language_id = new SelectList(languagesHandler.All(), "id", "title", site.language_id);
            return View(site);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sites site = sitesHandler.GetById(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            ViewBag.language_id = new SelectList(languagesHandler.All(), "id", "title", site.language_id);
            return View(site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,countContent,language_id")] Sites site)
        {
            if (ModelState.IsValid)
            {
                if (site.countContent >= 0)
                {
                    sitesHandler.Update(site);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.language_id = new SelectList(languagesHandler.All(), "id", "title", site.language_id);
            return View(site);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sites site = sitesHandler.GetById(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sitesHandler.Delete(id);
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
