using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Slider
    {
        public int Id { get; set; }

        public string Order { get; set; }

        public bool Visible { get; set; }
        public bool FloatRight { get; set; }

        public string CreateUser { get; set; }

        public DateTime CreateTime { get; set; }

        public string LastEditUser { get; set; }

        public DateTime LastEditTime { get; set; }
    }

    public class CrudSlider
    {
        public Slider Slider { get; set; }
        public Dictionary<string, SliderLanguage> SliderLanguage { get; set; }
        public Language Language { get; set; }
    }
}