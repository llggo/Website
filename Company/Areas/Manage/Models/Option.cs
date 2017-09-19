using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Company.Models;

namespace Company.Areas.Manage.Models
{
    public class Option
    {
        public int Id { get; set; }

        public bool ClientEnable { get; set; }
        public bool IntroEnable { get; set; }
        public bool SignUpEnable { get; set; }
        public bool OurProductsEnable { get; set; }
        public bool LiveChatEnable { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }

    public class CrudOption
    {
        public Option Option { get; set; }
        public Language Language { get; set; }
        public Dictionary<string,OptionLanguage> OptionLanguage { get; set; }
        public List<CrudSlider> CrudSlider { get; set; }
        public List<CrudClient> CrudClient { get; set; }
    }
}