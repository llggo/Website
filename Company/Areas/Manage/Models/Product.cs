using Company.Extends.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string LastEditUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_time")]
        public DateTime? LastEditTime { get; set; }

    }

    public class CrudProduct
    {
        public Product Product { get; set; }
        public Dictionary<string, ProductLanguage> ProductLanguage { get; set; }
        public Language Language { get; set; }
    }
}