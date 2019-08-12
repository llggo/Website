using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Areas.Manage.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Target { get; set; }
    }
}