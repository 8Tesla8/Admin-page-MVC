using System;
using Uspa.Domain.ModelCodeFirst.IdentityCodeFirst;

namespace Uspa.Domain.ModelCodeFirst
{
    public class Content //aump_content только одесса
    {
        public int id { get; set; }
        //public int siteId { get; set; }
        public virtual Site site { get; set; }

        public string title { get; set; } //маленькое название статьи
        public string introtext { get; set; } //название статьи
        public string fulltext { get; set; }
        public bool state { get; set; }

        public System.DateTime? created { get; set; }
        //public long createdByUserId { get; set; }
        public virtual User createdByUser { get; set; }

        public System.DateTime? modified { get; set; }
        //public long modifiedByUserId { get; set; }
        public virtual User modifiedByUser { get; set; }

        public System.DateTime? published { get; set; }
        //public int languageId { get; set; }
        //public virtual Language language { get; set; }

        //public int categoryId { get; set; }
        public virtual Category category { get; set; } //catid

        public DateTime? checkIn { get; set; }   //начала публикации
        public DateTime? checkOut { get; set; } //конец публикации

        //public virtual IEnumerable<UserGroup> allowedUserGroup { get; set; }
        //public Content()
        //{
        //    allowedUserGroup = new List<UserGroup>(); 
        //}
    }
}
