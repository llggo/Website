using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Areas.Manage.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Ảnh")]
        public string Image { get; set; }

        [Display(Name = "Mô tả ngắn gọn")]
        public string Describle { get; set; }

        [Display(Name = "Nội dung")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Thứ tự")]
        public int? Order { get; set; }

        [Display(Name = "Hiện lên")]
        public bool Visible { get; set; }

        [Display(Name = "Sửa")]
        public Sales.Models.Modified Edit { get; set; }

        [Display(Name = "Tạo")]
        public Sales.Models.Modified Create { get; set; }
    }
}