using System.Data;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.AdditionalFunc;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class ContentRepository : IContents
    {

        UspaIdentityDb db = new UspaIdentityDb();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(Contents content)
        {
            if (content != null)
            {
                db.Contents.Add(content);
                db.SaveChanges();
            }
        }

        public IQueryable<Contents> All()
        {
            return db.Contents
                .Include(c => c.Categories)
                .Include(c => c.Sites)
                .Include(c => c.Users)
                .Include(c => c.Users1)
            .OrderByDescending(c => c.created);
        }

        public void Delete(long? id)
        {
            Contents content = db.Contents.FirstOrDefault(c => c.id == id);

            if (content != null)
            {
                db.Entry(content).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void Delete(Contents content)
        {
            if (content != null)
            {
                db.Entry(content).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public Contents GetById(long? id)
        {
            return db.Contents.FirstOrDefault(c => c.id == id);
        }

        public IQueryable<Contents> Search(IQueryable<Contents> content, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                content = content.Where(c => c.title.Contains(search));
            }

            return content;
        }

        public IQueryable<Contents> Sorting(IQueryable<Contents> content, ref string sortingField, ref string prevState)
        {
            if (sortingField == null)
            {
                content = content.OrderBy(c => c.id);
            }
            else
            {
                if (sortingField.Equals("created"))
                {
                    content = content.SortingDate(ref sortingField, ref prevState);
                }
                if (sortingField.Equals("modified"))
                {
                    content = content.SortingDate(ref sortingField, ref prevState);
                }
            }

            return content;
        }

        public void Update(Contents content)
        {
            if (content != null)
            {
                db.Entry(content).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
