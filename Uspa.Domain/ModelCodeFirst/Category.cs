using System.Collections.Generic;
using Uspa.Domain.ModelCodeFirst.IdentityCodeFirst;

namespace Uspa.Domain.ModelCodeFirst
{
    public class Category//категории контента //ampu_category
    {
        public int id { get; set; }
        public string title { get; set; } //название title
        public int previd { get; set; }

        public System.DateTime? createdTime { get; set; }
        //public long createdByUserId { get; set; }
        public virtual User createdByUser { get; set; } //created_user_id id сначала брать со старой базы потом искать по мылу в новой

        //public int languageId { get; set; }
        public virtual Language language { get; set; }

        public bool published { get; set; }

        public virtual ICollection<Role> allowedUserGroup { get; set; } //разрешенные группы брать по id пользователю со старой базы
        public Category()
        {
            allowedUserGroup = new List<Role>();
        }
    }
}
