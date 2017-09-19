using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class CategoryLanguage
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}