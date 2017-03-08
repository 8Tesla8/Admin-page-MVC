using System.Data;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class MenusRepository : IMenus
    {
        UspaIdentityDb db = new UspaIdentityDb();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(Menus menu)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (menu != null)
                {
                    db.Menus.Add(menu);
                    db.SaveChanges();
                }
            }
        }

        public IQueryable<Menus> All()
        {
            return db.Menus.Include(m => m.Contents).Include(m => m.MenuTypes).Include(m => m.Sites).OrderBy(m => m.id);
        }

        public void Delete(long? id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                Menus menu = db.Menus.FirstOrDefault(u => u.id == id);

                if (menu != null)
                {
                    db.Entry(menu).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(Menus menu)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (menu != null)
                {
                    db.Entry(menu).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
        public Menus GetById(long? id)
        {
            return db.Menus.FirstOrDefault(u => u.id == id);

        }

        public void Update(Menus menu)
        {
            if (menu != null)
            {
                db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public IQueryable<Menus> Search(IQueryable<Menus> menu, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                menu = menu.Where(m => m.title.Contains(search));
            }

            return menu;
        }

        //Сортировка
        //public IQueryable<Menus> Sorting(IQueryable<Menus> menu, ref string sortingField, ref string prev)
        //{
        //    if (sortingField == null)
        //    {
        //        menu = menu.OrderBy(c => c.id);
        //    }
        //    else
        //    {
        //        if (sortingField.Equals("MenuType"))
        //        {
        //            menu = menu.SortingDate(ref sortingField, ref prev);
        //        }
        //        if (sortingField.Equals("Language"))
        //        {
        //            menu = menu.SortingDate(ref sortingField, ref prev);
        //        }
        //    }
        //    return menu;
        //}
    }
}
