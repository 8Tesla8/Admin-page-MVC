using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using Uspa.Domain.LocalDb;
using Uspa.Domain.ModelCodeFirst.IdentityCodeFirst;
using Uspa.Domain.Repository.Interface;

namespace Uspa.Domain.Repository.Implementation
{
    public class UsersGroupsRepository : IUserGroups
    {
        public void Add(IdentityRoles newUserGroup)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {

                var UserManager = new UserManager<User>(new UserStore<User>(db));


                if (newUserGroup != null)
                {
                    db.IdentityRoles.Add(newUserGroup);
                    db.SaveChanges();
                }
            }
        }
        public IEnumerable<IdentityRoles> All()
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.IdentityRoles.ToList();
            }
        }


        public void Delete(string id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                IdentityRoles deleteUserGroup = db.IdentityRoles.FirstOrDefault(ug => ug.Id.Equals(id));

                if (deleteUserGroup != null)
                {
                    db.Entry(deleteUserGroup).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();
                }
            }
        }
        public IdentityRoles GetById(string id)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                return db.IdentityRoles.FirstOrDefault(ug => ug.Id.Equals(id));
            }
        }
        public void Update(IdentityRoles userGroup)
        {
            using (UspaIdentityDb db = new UspaIdentityDb())
            {
                if (userGroup != null)
                {
                    db.Entry(userGroup).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

    }
}
