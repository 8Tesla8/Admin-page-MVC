using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class BannersRepository : IBanners
    {
        public void Add(Banners banner)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (banner != null)
                {
                    db.Banners.Add(banner);
                    db.SaveChanges();
                }
            }
        }

        public IEnumerable<Banners> All()
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.Banners.Include(c => c.Sites)
                .OrderByDescending(c => c.title).ToList();
            }
        }
        public void Delete(Banners banner)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (banner != null)
                {
                    db.Entry(banner).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
        public void Delete(long? id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                Banners deleteBanner = db.Banners.FirstOrDefault(ug => ug.id == id);

                if (deleteBanner != null)
                {
                    db.Entry(deleteBanner).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
        public Banners GetById(long? id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.Banners.Include(b => b.Sites).FirstOrDefault(ug => ug.id == id);
            }
        }
        public void Update(Banners banner)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (banner != null)
                {
                    db.Entry(banner).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}
