using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class LanguageLanguage
    {
        public int Id { get; set; }
        public int LanguageObjectId { get; set; }
        public int LanguageCheckId { get; set; }
        public string Name { get; set; }
    }

    public class LanguageView
    {
        public string CodeObject { get; set; }

        public string CodeCheck { get; set; }

        public string Name { get; set; }

        public string Info { get; set; }
        public int Id { get; internal set; }
    }
}