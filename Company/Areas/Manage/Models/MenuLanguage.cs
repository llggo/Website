using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class MenuLanguage
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
    }
}