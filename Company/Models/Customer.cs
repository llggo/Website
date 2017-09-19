using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public DateTime CreateTime { get; set; }
    }
}