using System.Collections.Generic;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface ILanguages
    {
        IEnumerable<Languages> All();
        Languages GetById(long? id);
    }
}
