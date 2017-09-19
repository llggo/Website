using Company.Extends.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Manage.Models
{
    public class ProductLanguage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int LanguageId { get; set; }

        public int Order { get; set; }

        public string Name { get; set; }

        public string Describle { get; set; }

    }
}