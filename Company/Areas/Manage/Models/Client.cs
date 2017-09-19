using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Order { get; set; }

        public bool Visible { get; set; }

        public string CreateUser { get; set; }

        public DateTime CreateTime { get; set; }

        public string LastEditUser { get; set; }

        public DateTime LastEditTime { get; set; }
    }

    public class CrudClient
    {
        public Client Client { get; set; }
        public Dictionary<string, ClientLanguage> ClientLanguage { get; set; }
        public Language Language { get; set; }
    }
}