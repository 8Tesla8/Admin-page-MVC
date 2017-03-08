using System;
using System.Collections.Generic;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface ISites : IDisposable
    {
        void Add(Sites site);
        IEnumerable<Sites> All();
        void Delete(Sites site);
        void Delete(long? id);
        Sites GetById(long? id);
        void Update(Sites banner);
    }
}
