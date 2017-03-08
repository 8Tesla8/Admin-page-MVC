using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class MenuTypesRepository : IMenuTypes
    {
        UspaIdentityDb db = new UspaIdentityDb();

        public void Dispose()
        {
            db.Dispose();
        }

        public IQueryable<MenuTypes> All()
        {
            return db.MenuTypes;
        }
    }
}
