using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Route { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }

    }
}