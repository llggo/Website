using Company.Extends.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class SProduct
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_user")]
        public string CreateUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_time")]
        public DateTime? CreateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_user")]
        public string LastEditUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_time")]
        public DateTime? LastEditTime { get; set; }
        public bool Enable { get; set; }
        public bool Slider { get; set; }

    }

    public class CrudSProduct
    {
        public SProduct SProduct { get; set; }
        public Dictionary<string, SProductLanguage> SProductLanguage { get; set; }
        public Language Language { get; set; }
    }
}