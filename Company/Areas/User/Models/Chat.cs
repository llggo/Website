using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Company.Areas.User.Models
{
    public class Chat
    {
        int Id { get; set; }
        string UserId { get; set; }
        string ToUserId { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public bool isSeed { get; set; }
    }
}