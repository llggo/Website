using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Language
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Info { get; set; }
    }

    public class CrudLanguage
    {
        public Language Language { get; set; }
        public Dictionary<string, LanguageLanguage> LanguageLanguage { get; set; }
    }
}