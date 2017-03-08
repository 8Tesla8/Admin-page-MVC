using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Uspa.Admin.ViewModel;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;
using Uspa.Domain.Settings;

namespace Uspa.Admin.Controllers
{
    public class ContentsController : Controller
    {
        private IContents _contentHandler;
        private ICategories _categoriesHandler;
        private ISites _sitesHandler;

        public ContentsController()
        {
            _contentHandler = new ContentRepository();
            _categoriesHandler = new CategoriesRepository();
            _sitesHandler = new SitesRepository();

            // Настройка AutoMapper
            Mapper.Initialize(cfg => cfg.CreateMap<Contents, ContentViewModel>()
            .ForMember("introtext", opt => opt.MapFrom(c => new HtmlString(c.introtext))));
        }

        public ActionResult Index(int? page, int? site, string search, string orderFeild, string prevOrder, int? category)
        {
            IQueryable<Contents> content = _contentHandler.All();

            //filter
            if (category != null && category != 0)
                content = content.Where(c => c.category_id == category);

            List<Categories> categories = _categoriesHandler.All().ToList();
            categories.Insert(0, new Categories { id = 0, title = "All" });
            ViewBag.category = new SelectList(categories, "id", "title");

            if (site != null && site != 0)
                content = content.Where(c => c.site_id == site);

            List<Sites> sites = _sitesHandler.All().ToList();
            sites.Insert(0, new Sites { id = 0, title = "All" });
            ViewBag.site = new SelectList(sites, "id", "title");

            //search
            content = _contentHandler.Search(content, search);

            //sorting
            content = _contentHandler.Sorting(content, ref orderFeild, ref prevOrder);

            //сохранение состояния выбора пользователя
            ViewBag.SearchState = search;
            ViewBag.PrevState = prevOrder;
            ViewBag.CategoryState = category;
            ViewBag.SiteState = site;

            var contentMapper =
                Mapper.Map<IEnumerable<Contents>, List<ContentViewModel>>(content);

            int pageSize = PagingSettings.PageSizeInContent;
            int pageNumber = (page ?? 1);

            return View(contentMapper.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contents content = _contentHandler.GetById(id);
            var contentMapper =
                Mapper.Map<Contents, ContentViewModel>(content);

            if (content == null)
            {
                return HttpNotFound();
            }
            return View(contentMapper);
        }

        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(_categoriesHandler.All(), "id", "title");
            ViewBag.site_id = new SelectList(_sitesHandler.All(), "id", "title");

            //ViewBag.createdByUser_id = new SelectList(db.Users, "id", "name");
            //ViewBag.modifiedByUser_id = new SelectList(db.Users, "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,introtext,fulltext,state,created,modified,published,checkIn,checkOut,site_id,createdByUser_id,modifiedByUser_id,category_id")] Contents contents)
        {
            //TODO добавить юзера добавившего контент
            //TODO сделать проверку языка, чтобы у категории и сайта были одинаковые языки
            if (ModelState.IsValid)
            {
                contents.created = DateTime.Now;

                _contentHandler.Add(contents);
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(_categoriesHandler.All(), "id", "title", contents.category_id);
            ViewBag.site_id = new SelectList(_sitesHandler.All(), "id", "title", contents.site_id);
            //ViewBag.createdByUser_id = new SelectList(db.Users, "id", "name", contents.createdByUser_id);
            //ViewBag.modifiedByUser_id = new SelectList(db.Users, "id", "name", contents.modifiedByUser_id);
            return View(contents);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contents content = _contentHandler.GetById(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(_categoriesHandler.All(), "id", "title", content.category_id);
            ViewBag.site_id = new SelectList(_sitesHandler.All(), "id", "title", content.site_id);

            return View(content);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,introtext,fulltext,state,created,modified,published,checkIn,checkOut,site_id,createdByUser_id,modifiedByUser_id,category_id")] Contents content)
        {

            //TODO добавить юзера изменившего контент
            //TODO сделать проверку языка, чтобы у категории и сайта были одинаковые языки

            if (ModelState.IsValid)
            {
                content.modified = DateTime.Now;
                _contentHandler.Update(content);
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(_categoriesHandler.All(), "id", "title", content.category_id);
            ViewBag.site_id = new SelectList(_sitesHandler.All(), "id", "title", content.site_id);

            return View(content);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contents content = _contentHandler.GetById(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            return View(content);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _contentHandler.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contentHandler.Dispose();
                _categoriesHandler.Dispose();
                _sitesHandler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
