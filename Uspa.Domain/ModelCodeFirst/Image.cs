using Uspa.Domain.ModelCodeFirst.IdentityCodeFirst;

namespace Uspa.Domain.ModelCodeFirst
{
    public class Image
    {
        public long id { get; set; }
        public string title { get; set; } //alias
        public string filePath { get; set; } //filename

        public System.DateTime? created { get; set; }
        //public long createdByUserId { get; set; }
        public virtual User createdByUser { get; set; }

        public virtual Album album { get; set; }

        //public System.DateTime published { get; set; }
        ////public long publishededByUserId { get; set; }
        //public virtual User publishededByUser { get; set; }

        public bool state { get; set; }
    }
}
