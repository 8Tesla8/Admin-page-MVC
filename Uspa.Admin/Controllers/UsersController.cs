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
    public class UsersController : Controller
    {
        private IUsers usersHandler = new UsersRepository();
        private IUserGroups userGroupsHandler = new UsersGroupsRepository();

        public ActionResult Index(int? page, string search,
            string orderFeild, string prevOrder,     //for sorting
            string userGroup) //filter
        {
            var users = usersHandler.All();

            //filter
            if (userGroup != null && userGroup != null)
                users = users.Where(u => u.IdentityUserRoles.Any(ug => ug.IdentityRoles.Name == userGroup));

            List<IdentityRoles> userGroups = new UsersGroupsRepository().All().ToList();
            userGroups.Insert(0, new IdentityRoles { Id = "0", Name = "All" });
            ViewBag.UserGroup = new SelectList(userGroups, "id", "title");

            //search
            users = usersHandler.Search(users, search);

            //sorting
            users = usersHandler.Sorting(users, ref orderFeild, ref prevOrder);

            //сохранение состояния выбора пользователя
            ViewBag.SearchState = search;
            ViewBag.PrevState = prevOrder;
            ViewBag.UsGrState = userGroup;

            int pageSize = PagingSettings.PageSizeInUsers;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersHandler.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            ViewBag.UserGroups = new MultiSelectList(userGroupsHandler.All(), "id", "title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,userName,email,password,registerDate,lastVisitDate,block")] Users user)
        {
            if (ModelState.IsValid)
            {

                usersHandler.Add(user);
                return RedirectToAction("Index");
            }

            ViewBag.UserGroups = new MultiSelectList(userGroupsHandler.All(), "id", "title", user.IdentityUserRoles);
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersHandler.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,prevId,name,userName,email,password,registerDate,lastVisitDate,block")] Users user)
        {
            if (ModelState.IsValid)
            {
                usersHandler.Update(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users user = usersHandler.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            usersHandler.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                usersHandler.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
