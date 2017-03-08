using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Uspa.Domain.ModelCodeFirst;
using Uspa.Domain.ModelCodeFirst.IdentityCodeFirst;

namespace Uspa.Admin.ViewModel
{
    public class ContentViewModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Сайт города ")]
        public virtual Site site { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Контент")]
        public IHtmlString introtext { get; set; }

        [Display(Name = "Публикация")]
        public bool state { get; set; }

        [Display(Name = "Дата создания")]
        public System.DateTime? created { get; set; }

        [Display(Name = "Автор")]
        public virtual User createdByUser { get; set; }

        [Display(Name = "Дата изменения")]
        public System.DateTime? modified { get; set; }

        [Display(Name = "Изменено")]
        public virtual User modifiedByUser { get; set; }

        [Display(Name = "Дата публикации")]
        public System.DateTime? published { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public virtual Category category { get; set; }

        [Display(Name = "Добавить на сайт")]
        public DateTime? checkIn { get; set; }

        [Display(Name = "Убрать с сайта")]
        public DateTime? checkOut { get; set; }
    }
}