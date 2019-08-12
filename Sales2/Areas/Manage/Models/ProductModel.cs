using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales.Areas.Manage.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Danh mục")]
        public int CategoryId { get; set; }

        [Display(Name = "Tên")]
        public string Name { get; set; }

        [Display(Name = "Ảnh")]
        public string[] Image { get; set; }

        [Display(Name = "Mô tả ngắn gọn")]
        public string Describle { get; set; }

        [Display(Name = "Mô tả chi tiết")]
        public string Infomation { get; set; }

        [Display(Name = "Thứ tự")]
        public int? Order { get; set; }

        [Display(Name = "Hiện lên")]
        public bool Visible { get; set; }

        [Display(Name = "Giá")]
        public float? Price { get; set; }

        [Display(Name = "Giảm giá")]
        public float? Discount { get; set; }

        [Display(Name = "Sửa")]
        public Sales.Models.Modified Edit { get; set; }

        [Display(Name = "Tạo")]
        public Sales.Models.Modified Create { get; set; }
    }

    public class ProductCrud
    {
        public ProductCrud()
        {
            Product = new Product();
            Images = new List<string>();
        }
        public Product Product { get; set; }
        public List<string> Images { get; set; }
    }

    public class ProducImageView
    {
        public ProducImageView()
        {
            Product = new Product();
            Image = new ProductImage();
        }

        public Product Product { get; set; }
        public ProductImage Image { get; set; }
    }
}