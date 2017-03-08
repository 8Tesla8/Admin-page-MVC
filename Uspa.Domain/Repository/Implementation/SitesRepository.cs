using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class SitesRepository : ISites
    {
        UspaIdentityDb db = new UspaIdentityDb();

        public void Dispose()
        {
            db.Dispose();
        }

        public void Add(Sites site)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (site != null)
                {
                    db.Sites.Add(site);
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<Sites> All()
        {
            return db.Sites.Include(s => s.Languages)
            .OrderByDescending(c => c.title).ToList();
        }

        public void Delete(long? id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                Sites site = db.Sites.FirstOrDefault(ug => ug.id == id);

                if (site != null)
                {
                    db.Entry(site).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public void Delete(Sites site)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (site != null)
                {
                    db.Entry(site).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }

        public Sites GetById(long? id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.Sites.Include(s => s.Languages).FirstOrDefault(ug => ug.id == id);
            }
        }

        public void Update(Sites site)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (site != null)
                {
                    db.Entry(site).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
