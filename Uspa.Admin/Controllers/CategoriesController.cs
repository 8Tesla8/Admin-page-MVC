using PagedList;
using System;
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
    public class CategoriesController : Controller
    {
        private ICategories repository = new CategoriesRepository();
        private ILanguages _languagesHandler = new LanguagesRepository();
        private IUserGroups _usergroupsHandler = new UsersGroupsRepository();

        public ActionResult Index(int? page, string search,
            string orderFeild, string prevOrder,     //for sorting
            int? language, string userGroup)  //for filter 
        {
            var categories = repository.All();

            //TODO показывать UserGroup - in create сделать выбор UserGroups

            //TODO доделать view что бы отображалось UserGroups in Edit Create Deteils

            //TODO использовать написсанные интерфейсы
            //filter
            if (language != null && language != 0)
                categories = categories.Where(c => c.language_id == language);

            List<Languages> languages = _languagesHandler.All().ToList();
            languages.Insert(0, new Languages { id = 0, title = "All" });
            ViewBag.Language = new SelectList(languages, "id", "title");

            if (userGroup != null && userGroup != null)
                categories = categories.Where(c => c.IdentityRoles.Any(ug => ug.Id == userGroup));

            List<IdentityRoles> userGroups = _usergroupsHandler.All().ToList();
            userGroups.Insert(0, new IdentityRoles { Id = "0", Name = "All" });
            ViewBag.UserGroup = new SelectList(userGroups, "id", "title");


            //serch
            categories = repository.Search(categories, search);


            //sorting
            categories = repository.Sorting(categories, ref orderFeild, ref prevOrder);

            //сохранение состояния выбора пользователя
            ViewBag.SearchState = search;
            ViewBag.PrevState = prevOrder;
            ViewBag.LangState = language;
            ViewBag.UsGrState = userGroup;


            int pageSize = PagingSettings.PageSizeInCategories;
            int pageNumber = (page ?? 1);
            return View(categories.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories category = repository.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        public ActionResult Create()
        {
            ViewBag.language_id = new SelectList(_languagesHandler.All(), "id", "title");
            ViewBag.createdByUser_id = new SelectList(_usergroupsHandler.All(), "id", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,createdTime,published,createdByUser_id,language_id")] Categories category) //[Bind(Include = "id,title,previd,createdTime,published,createdByUser_id,language_id")]
        {
            //TODO при создании указать пользователя который создал эту тему

            if (ModelState.IsValid)
            {
                category.createdTime = DateTime.Now;

                repository.Add(category);
                return RedirectToAction("Index");
            }

            ViewBag.language_id = new SelectList(_languagesHandler.All(), "id", "title", category.language_id);
            ViewBag.createdByUser_id = new SelectList(_usergroupsHandler.All(), "id", "name", category.createdByUser_Id);
            return View(category);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories category = repository.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.language_id = new SelectList(_languagesHandler.All(), "id", "title", category.language_id);
            ViewBag.createdByUser_id = new SelectList(_usergroupsHandler.All(), "id", "name", category.createdByUser_Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,previd,createdTime,published,createdByUser_id,language_id")] Categories category)
        {
            if (ModelState.IsValid)
            {
                repository.Update(category);

                return RedirectToAction("Index");
            }
            ViewBag.language_id = new SelectList(_languagesHandler.All(), "id", "title", category.language_id);
            ViewBag.createdByUser_id = new SelectList(_usergroupsHandler.All(), "id", "name", category.createdByUser_Id);
            return View(category);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories category = repository.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
