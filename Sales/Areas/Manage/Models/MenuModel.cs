using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales.Areas.Manage.Models
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int Target { get; set; }
    }
}