using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Uspa.Domain.LocalDb;

namespace Uspa.Admin.ViewModel
{
    public class BannersViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Link")]
        public string url { get; set; }

        public bool state { get; set; }

        public Nullable<int> site_id { get; set; }

    }
}