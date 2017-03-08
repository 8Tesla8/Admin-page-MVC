using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uspa.Domain.ModelCodeFirst
{
    public class Banner //рекламный баннер
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public bool state { get; set; }

        //public int siteId { get; set; }
        public virtual Site site { get; set; }
    }
}