using System;
using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IMenuTypes : IDisposable
    {
        IQueryable<MenuTypes> All();
    }
}
