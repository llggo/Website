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

        public string LogoProducts { get; set; }

        public string WhyUs { get; set; }
        public string OurProductsDes { get; set; }
        public string IntroTitle { get; set; }
        public string IntroDescribe { get; set; }
        [AllowHtml]
        public string SignUpTitle { get; set; }
        public string SignUpDescribe { get; set; }

        public string AboutUs { get; set; }

        public string AboutUsTarget { get; set; }

        public string Phone { get; set; }
        public string Phone2 { get; set; }

        public string Email { get; set; }
        public string Email2 { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }
}