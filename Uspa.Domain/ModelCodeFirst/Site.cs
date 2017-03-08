using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uspa.Domain.ModelCodeFirst
{
    public class Site //blank брать русские названия, site config создавать города по url
    {
        public int id { get; set; }
        public string title { get; set; }
        public int countContent { get; set; }//количество новостей на сайте

        public virtual Language language { get; set; }

    }
}
