using System.Net;
using System.Web.Mvc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Implementation;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Admin.Controllers
{
    public class UserGroupsController : Controller
    {
        private IUserGroups repository = new UsersGroupsRepository();

        public ActionResult Index()
        {
            return View(repository.All());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRoles userGroup = repository.GetById(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title")] IdentityRoles userGroup)
        {
            if (ModelState.IsValid)
            {
                repository.Add(userGroup);
                return RedirectToAction("Index");
            }

            return View(userGroup);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRoles userGroup = repository.GetById(id);
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title")] IdentityRoles userGroup)
        {
            if (ModelState.IsValid)
            {
                repository.Update(userGroup);

                return RedirectToAction("Index");
            }
            return View(userGroup);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRoles userGroups = repository.GetById(id);
            if (userGroups == null)
            {
                return HttpNotFound();
            }
            return View(userGroups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            repository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
