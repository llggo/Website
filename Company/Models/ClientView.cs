using Company.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Models
{
    public class ClientView
    {
        public Client Client { get; set; }
        public ClientLanguage ClientLanguage { get; set; }
    }
}