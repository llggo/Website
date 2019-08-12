using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Models
{
    public class HomeView
    {
        public List<Areas.Manage.Models.Slider> Slider { get; set; }
        public List<Areas.Manage.Models.ProducImageView> FeaturedProducts{ get;set;}
        public List<Areas.Manage.Models.ProducImageView> LastestProducts { get; set; }

        public List<Areas.Manage.Models.Article> LastestArticle { get; set; }

    }
}