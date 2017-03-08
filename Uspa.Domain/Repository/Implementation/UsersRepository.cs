using System.Data;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.AdditionalFunc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class UsersRepository : IUsers
    {
        UspaDbEntities db = new UspaDbEntities();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(Users user)
        {
            using (UspaDbEntities db = new UspaDbEntities())
            {
                if (user != null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
        }

        public IQueryable<Users> All()
        {
            return db.Users.Include(c => c.IdentityUserRoles).OrderByDescending(u => u.name);
        }

        public void Delete(string id)
        {
            using (UspaDbEntities db = new UspaDbEntities())
            {
                Users user = db.Users.FirstOrDefault(u => u.Id == id);

                if (user != null)
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(Users user)
        {
            using (UspaDbEntities db = new UspaDbEntities())
            {
                if (user != null)
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public Users GetById(string id)
        {
            using (UspaDbEntities db = new UspaDbEntities())
            {
                return db.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public IQueryable<Users> Search(IQueryable<Users> users, string search)
        {

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.name.Contains(search));
            }

            return users;

        }

        public IQueryable<Users> Sorting(IQueryable<Users> users, ref string sortingField, ref string prev)
        {
            if (sortingField == null)
            {
                users = users.OrderBy(c => c.Id);
            }
            else
            {
                if (sortingField.Equals("name"))
                {
                    users = users.SortingDate(ref sortingField, ref prev);
                }
                if (sortingField.Equals("email"))
                {
                    users = users.SortingString(ref sortingField, ref prev);
                }
                if (sortingField.Equals("registerDate"))
                {
                    users = users.SortingDate(ref sortingField, ref prev);
                }
                if (sortingField.Equals("lastVisitDate"))
                {
                    users = users.SortingDate(ref sortingField, ref prev);
                }
            }

            return users;
        }

        public void Update(Users user)
        {
            using (UspaDbEntities db = new UspaDbEntities())
            {
                if (user != null)
                {
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
