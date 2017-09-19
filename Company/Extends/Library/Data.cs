using Company.Areas.Manage.Models;
using Company.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Principal;
using System.Text;

namespace Company.Extends.Library
{
    public class Data
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;
        public OptionView OptionView()
        {
            return (from o in db.Option
                     join ol in db.OptionLanguage on o.Id equals ol.OptionId
                     join l in db.Language on ol.LanguageId equals l.Id
                     where l.Code == language
                     select new OptionView
                     {
                         Option = o,
                         OptionLanguage = ol
                     }).FirstOrDefault();
        }

        public string ToUrlFriendly(string value)
        {
            if(value == null)
            {
                return value;
            }
            var item = value.Trim().ToLower();
            var builder = new StringBuilder();


            foreach (var c in item)
            {
                if (c == ' ')
                {
                    builder.Append("-");
                }
                else if (c == '&')
                {
                    builder.Append("and");
                }
                else
                {
                    if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z'))
                    {
                        builder.Append(c);
                    }
                }
            }
            return builder.ToString();
        }


    }
}