using System.Data.Entity;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Model
{
    public class ModelDb : IModel
    {
        public DbContext GetModelDb()
        {
            return new UspaIdentityDb();
        }
    }
}
