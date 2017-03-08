using PagedList;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;
using Uspa.Domain.Settings;

namespace Uspa.Admin.Controllers
{
    public class MenusController : Controller
    {
        private IMenus menusHandler = new MenusRepository();
        private IMenuTypes menutypesHandler = new MenuTypesRepository();
        private ISites sitesHandler = new SitesRepository();
        private IContents _contentsHandler = new ContentRepository();

        public ActionResult Index(int? page, string search, int? menuType)
        {
            var menus = menusHandler.All();

            //filter
            if (menuType != null && menuType != 0)
                menus = menus.Where(m => m.menuType_id == menuType);

            List<MenuTypes> menuTypes = new MenuTypesRepository().All().ToList();
            menuTypes.Insert(0, new MenuTypes { id = 0, title = "All" });
            ViewBag.MenuType = new SelectList(menuTypes, "id", "title");

            //serch
            menus = menusHandler.Search(menus, search);

            int pageSize = PagingSettings.PageSizeInMenu;
            int pageNumber = (page ?? 1);
            return View(menus.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Menus menus = menusHandler.GetById(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            return View(menus);
        }

        public ActionResult Create()
        {
            ViewBag.сontent_id = new SelectList(_contentsHandler.All(), "id", "title");
            ViewBag.menuType_id = new SelectList(menutypesHandler.All(), "id", "title");
            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,previd,parentidPrev,parentid,title,state,menuType_id,site_id,сontent_id")] Menus menus)
        {
            if (ModelState.IsValid)
            {
                menusHandler.Add(menus);
                return RedirectToAction("Index");
            }

            ViewBag.сontent_id = new SelectList(_contentsHandler.All(), "id", "title", menus.сontent_id);
            ViewBag.menuType_id = new SelectList(menutypesHandler.All(), "id", "title", menus.menuType_id);
            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title", menus.site_id);
            return View(menus);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menus menus = menusHandler.GetById(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            ViewBag.сontent_id = new SelectList(_contentsHandler.All(), "id", "title", menus.сontent_id);
            ViewBag.menuType_id = new SelectList(menutypesHandler.All(), "id", "title", menus.menuType_id);
            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title", menus.site_id);
            return View(menus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,previd,parentidPrev,parentid,title,state,menuType_id,site_id,сontent_id")] Menus menus)
        {
            if (ModelState.IsValid)
            {
                menusHandler.Update(menus);
                return RedirectToAction("Index");
            }
            ViewBag.сontent_id = new SelectList(_contentsHandler.All(), "id", "title", menus.сontent_id);
            ViewBag.menuType_id = new SelectList(menutypesHandler.All(), "id", "title", menus.menuType_id);
            ViewBag.site_id = new SelectList(sitesHandler.All(), "id", "title", menus.site_id);
            return View(menus);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menus menus = menusHandler.GetById(id);
            if (menus == null)
            {
                return HttpNotFound();
            }
            return View(menus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menusHandler.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                menusHandler.Dispose();
                menutypesHandler.Dispose();
                sitesHandler.Dispose();
                _contentsHandler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
