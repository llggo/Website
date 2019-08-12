using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Manage.Models
{
    public class OptionLanguage
    {
        public int Id { get; set; }

        public int OptionId { get; set; }

        public int LanguageId { get; set; }

        public string LogoPath { get; set; }

        public string LogoBottomPath { get; set; }

        public string AboutUs { get; set; }

        public string AboutUsTarget { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}