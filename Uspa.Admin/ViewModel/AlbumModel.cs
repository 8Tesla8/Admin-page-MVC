using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Uspa.Admin.ViewModel
{
    public class AlbumViewModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Имя альбома")]
        public string title { get; set; }

        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        [Display(Name = "Язык")]
        public Nullable<int> language_id { get; set; }

        public IEnumerable<SelectListItem> ActionsList { get; set; }

    }


}