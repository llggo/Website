using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Menu
    {
        public int Id { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        public bool isDropdown { get; set; }

        public bool isActive { get; set; }

        public int? Order { get; set; }

        public bool Visible { get; set; }

        public string Target { get; set; }

        public string CreateUser { get; set; }

        public DateTime CreateTime { get; set; }

        public string LastEditUser { get; set; }

        public DateTime LastEditTime { get; set; }

    }

    public class CrudMenu 
    {
        public Menu Menu { get; set; }
        public Language Language { get; set; }
        public Dictionary<string, MenuLanguage> MenuLanguage { get; set; }
    }
}