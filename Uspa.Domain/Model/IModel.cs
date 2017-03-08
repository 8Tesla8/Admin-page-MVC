using System.Data.Entity;

namespace Uspa.Domain.Model
{
    public interface IModel
    {
        DbContext GetModelDb();
    }
}
