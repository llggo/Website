using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class SpecificProductsView
    {
        public SProduct SProduct { get; set; }
        public SProductLanguage SProductLanguage { get; set; }
    }
}