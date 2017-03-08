using System.Data;
using System.Data.Entity;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;


namespace Uspa.Domain.Repository.Implementation
{
    public class AlbumRepository : IAlbum
    {
        UspaIdentityDb db = new UspaIdentityDb();

        public void Add(Albums album)
        {
            if (album != null)
            {
                db.Albums.Add(album);
                db.SaveChanges();
            }
        }

        public IQueryable<Albums> All()
        {

            return db.Albums.OrderBy(m => m.id);

        }

        public void Delete(long? id)
        {

            Albums album = db.Albums.FirstOrDefault(al => al.id == id);
            if (album != null)
            {
                db.Entry(album).State = EntityState.Deleted;
                db.SaveChanges();
            }

        }

        public void Delete(Albums album)
        {

            if (album != null)
            {
                db.Entry(album).State = EntityState.Deleted;
                db.SaveChanges();
            }

        }

        public Albums GetById(long? id)
        {

            return db.Albums.FirstOrDefault(al => al.id == id);

        }

        public void Update(Albums album)
        {
            if (album != null)
            {

                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IQueryable<Albums> Search(IQueryable<Albums> album, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                album = album.Where(m => m.title.Contains(search));
            }
            return album;
        }
    }
}
