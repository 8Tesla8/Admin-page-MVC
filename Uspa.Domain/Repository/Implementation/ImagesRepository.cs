using System.Data.Entity;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class ImagesRepository : IImage
    {
        UspaIdentityDb db = new UspaIdentityDb();

        public void Add(Images image)
        {
            if (image != null)
            {
                db.Images.Add(image);
                db.SaveChanges();
            }
        }

        public IQueryable<Images> All()
        {
            return db.Images.Include(i => i.Albums).Include(i => i.Users).OrderBy(m => m.id);
        }

        public void Delete(long? id)
        {
            Images image = db.Images.FirstOrDefault(im => im.id == id);
            if (image != null)
            {
                db.Entry(image).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void Delete(Images image)
        {
            if (image != null)
            {
                db.Entry(image).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Images GetById(long? id)
        {
            return db.Images.FirstOrDefault(im => im.id == id);
        }

        public IQueryable<Images> Search(IQueryable<Images> image, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                image = image.Where(m => m.title.Contains(search));
            }
            return image;
        }

        public void Update(Images image)
        {
            if (image != null)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
