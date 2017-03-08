using System;
using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IImage : IDisposable
    {
        void Add(Images image);
        IQueryable<Images> All();
        void Delete(Images image);
        void Delete(long? id);
        Images GetById(long? id);
        void Update(Images image);
        IQueryable<Images> Search(IQueryable<Images> image, string search);
    }
}
