using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class ArticleView
    {
        public Article Article { get; set; }
        public ArticleLanguage ArticleLanguage { get; set; }
    }
}