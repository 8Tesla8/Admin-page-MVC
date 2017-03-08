using System;
using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface ICategories : IDisposable
    {
        void Add(Categories newCategory);
        IQueryable<Categories> All();
        void Delete(int? id);
        Categories GetById(int? id);
        void Update(Categories Category);

        IQueryable<Categories> Search(IQueryable<Categories> categories, string search);

        IQueryable<Categories> Sorting(IQueryable<Categories> categories,
            ref string sortingField, ref string prevState);
    }
}
