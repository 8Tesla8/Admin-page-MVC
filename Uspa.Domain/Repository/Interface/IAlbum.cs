using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IAlbum
    {
        void Add(Albums album);
        IQueryable<Albums> All();
        void Delete(Albums album);
        void Delete(long? id);
        Albums GetById(long? id);
        void Update(Albums album);
        void Dispose();
        IQueryable<Albums> Search(IQueryable<Albums> menu, string search);
    }
}
