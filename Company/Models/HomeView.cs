using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class HomeView
    {
        public List<ArticleView> SpecificProductsView { get; set; }
        public List<ArticleView> ArticleViewComments { get; set; }
        public List<ClientView> ClientView { get; set; }
        public List<ArticleView> SliderView { get; set; }
        public OptionView OptionView { get; set; }
    }
}