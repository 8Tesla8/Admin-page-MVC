using System.Linq;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IUsers
    {
        void Add(Users users);
        IQueryable<Users> All();
        void Delete(Users users);
        void Delete(string id);
        Users GetById(string id);
        void Update(Users users);
        void Dispose();

        IQueryable<Users> Search(IQueryable<Users> users, string search);
        IQueryable<Users> Sorting(IQueryable<Users> users, ref string sortingField, ref string prevState);
    }
}
