using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class SliderLanguage
    {
        public int Id { get; set; }
        public int SliderId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string Infomation { get; set; }
        public string Target { get; set; }
    }
}