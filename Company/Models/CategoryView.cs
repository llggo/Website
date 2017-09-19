using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class CategoryView
    {
        public Category Category { get; set; }
        public CategoryLanguage CategoryLanguage { get; set; }
    }
}