//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Uspa.Domain.ModelCodeFirst
//{
//    public class User
//    {
//        public int prevId { get; set; }

//        public int id { get; set; }
//        public string name { get; set; }
//        public string userName { get; set; }
//        public string email { get; set; }
//        public string password { get; set; }

//        //public int userGroupId { get; set; }
//        public virtual ICollection<UserGroup> userGroup { get; set; }

//        public System.DateTime? registerDate { get; set; }
//        public System.DateTime? lastVisitDate { get; set; }
//        public bool block { get; set; }

//        public User()
//        {
//            userGroup = new List<UserGroup>();
//        }
//    }
//}
