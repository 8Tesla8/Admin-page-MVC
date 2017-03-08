using System.Collections.Generic;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class LanguagesRepository : ILanguages
    {
        public IEnumerable<Languages> All()
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.Languages.OrderByDescending(l => l.title).ToList();
            }
        }

        public Languages GetById(long? id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.Languages.FirstOrDefault(l => l.id == id);
            }
        }
    }
}
