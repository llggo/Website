using Company.Extends.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Manage.Models
{
    public class ArticleLanguage
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public int LanguageId { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_articale_language_name")]
        public string Name { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_articale_language_describle")]
        public string Describe { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_articale_language_content")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_articale_language_image")]
        public string Image { get; set; }
    }
}