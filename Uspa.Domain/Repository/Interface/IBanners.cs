using System.Collections.Generic;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IBanners
    {
        void Add(Banners banner);
        IEnumerable<Banners> All();
        void Delete(Banners banner);
        void Delete(long? id);
        Banners GetById(long? id);
        void Update(Banners banner);
    }
}
