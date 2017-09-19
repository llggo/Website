using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class MenuView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public bool isDropdown { get; set; }
        public string Target { get; set; }
    }
}