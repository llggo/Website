using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales.Areas.Manage.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Menu gốc")]
        public bool IsRoot { get; set; }
        [Display(Name = "Menu cha")]
        public bool IsParent { get; set; }

        [Display(Name = "Menu cha")]
        public int? ParentId { get; set; }
        [Display(Name = "Thứ tự")]
        public int? Order { get; set; }
        [Display(Name = "Đường dẫn")]
        public string Target { get; set; }
        [Display(Name = "Hiện lên")]
        public bool Visible { get; set; }
        [Display(Name = "Sửa")]
        public Sales.Models.Modified Edit { get; set; }
        [Display(Name = "Tạo")]
        public Sales.Models.Modified Create { get; set; }
    }
}