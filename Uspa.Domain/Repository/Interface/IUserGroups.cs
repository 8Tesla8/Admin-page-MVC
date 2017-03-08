using System.Collections.Generic;
using Uspa.Domain.LocalDb;

namespace Uspa.Domain.Repository.Interface
{
    public interface IUserGroups
    {
        void Add(IdentityRoles newUserGroup);
        IEnumerable<IdentityRoles> All();
        void Delete(string id);
        IdentityRoles GetById(string id);
        void Update(IdentityRoles userGroup);
    }
}
