//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Uspa.Domain.ModelCodeFirst
//{
//    public class UserGroup
//    {
//        public long id { get; set; }
//        public string title { get; set; }
//        public virtual ICollection<Category> userGroupCategories { get; set; } 
//        public virtual ICollection<User> users { get; set; } 
//        public UserGroup()
//        {
//            userGroupCategories = new List<Category>();
//            users = new List<User>();
//        }
//    }
//}