using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class ProductsView
    {
        public List<Products> Products { get; set; }
        public string Logo { get; set; }
    }

    public class Products
    {
        public Product Product { get; set; }
        public ProductLanguage ProducLanguage { get; set; }
        public List<ProductImage> ProductImage { get; set; }
    }
}