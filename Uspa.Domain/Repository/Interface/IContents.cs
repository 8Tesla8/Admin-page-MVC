using System;
using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IContents : IDisposable
    {
        void Add(Contents content);
        IQueryable<Contents> All();
        void Delete(Contents content);
        void Delete(long? id);
        Contents GetById(long? id);
        void Update(Contents content);

        IQueryable<Contents> Search(IQueryable<Contents> content, string search);
        IQueryable<Contents> Sorting(IQueryable<Contents> content, ref string sortingField, ref string prevState);
    }
}
