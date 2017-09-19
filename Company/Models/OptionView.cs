using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class OptionView
    {
        public Option Option { get; set; }
        public OptionLanguage OptionLanguage { get; set; }
    }
}