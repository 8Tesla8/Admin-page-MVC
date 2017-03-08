using System.Data;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.AdditionalFunc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class CategoriesRepository : ICategories
    {
        UspaIdentityDb db = new UspaIdentityDb();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(Categories newCategory)
        {
            if (newCategory != null)
            {
                db.Categories.Add(newCategory);
                db.SaveChanges();
            }
        }

        public IQueryable<Categories> Search(IQueryable<Categories> categories, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(c => c.title.Contains(search));
            }

            return categories;
        }

        public IQueryable<Categories> All()
        {
            return db.Categories.Include(c => c.Languages).Include(c => c.Users).Include(c => c.IdentityRoles);
        }

        public IQueryable<Categories> Sorting(IQueryable<Categories> categories, ref string sortingField, ref string prev)
        {
            if (sortingField == null)
            {
                categories = categories.OrderBy(c => c.id);
            }
            else
            {
                if (sortingField.Equals("user"))
                {
                    if (!string.IsNullOrEmpty(prev))
                    {
                        if (prev.Contains(sortingField))
                        {
                            if (prev[prev.Length - 1] == 'A')
                            {
                                prev = sortingField + 'D';
                                categories = categories.OrderByDescending(c => c.Users.name);
                            }
                            else if (prev[prev.Length - 1] == 'D')
                            {
                                prev = sortingField + 'A';
                                categories = categories.OrderBy(c => c.Users.name);
                            }
                            else
                            {
                                prev = sortingField + 'D';
                                categories = categories.OrderByDescending(c => c.Users.name);
                            }
                        }
                        else
                        {
                            categories = categories.OrderBy(c => c.Users.name);
                            prev = sortingField + 'A';
                        }
                    }
                    else
                    {
                        categories = categories.OrderBy(c => c.title);
                        prev = sortingField + 'A';
                    }
                }
                if (sortingField.Equals("createdTime"))
                {
                    categories = categories.SortingDate(ref sortingField, ref prev);
                }
                if (sortingField.Equals("title"))
                {
                    categories = categories.SortingString(ref sortingField, ref prev);
                }
            }

            return categories;
        }

        public void Delete(int? id)
        {
            Categories category = db.Categories.FirstOrDefault(c => c.id == id);

            if (category != null)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Categories GetById(int? id)
        {
            return db.Categories.FirstOrDefault(c => c.id == id);
        }

        public void Update(Categories category)
        {
            if (category != null)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
