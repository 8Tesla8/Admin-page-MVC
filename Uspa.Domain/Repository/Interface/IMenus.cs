using System;
using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IMenus : IDisposable
    {
        void Add(Menus menu);
        IQueryable<Menus> All();
        void Delete(Menus menu);
        void Delete(long? id);
        Menus GetById(long? id);
        void Update(Menus menu);

        IQueryable<Menus> Search(IQueryable<Menus> menu, string search);
        //IQueryable<Menus> Sorting(IQueryable<Menus> menu, ref string sortingField, ref string prev);
    }
}
