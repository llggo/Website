using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Threading;
using System.Collections;
using Company.Models;
using Company.Areas.Manage.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Company.Extends.Library
{
    public class Language
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<LanguageView> LanguageList( string currentLanguage, out string languageDbName)
        {
            var list =  (from li in db.Language
                         join ll in db.LanguageLanguage
                         on li.Id equals ll.LanguageCheckId
                         join lo in db.Language on ll.LanguageObjectId equals lo.Id
                         where li.Code == currentLanguage
                         select new LanguageView
                         {
                             Id = lo.Id,
                             CodeObject = li.Code,
                             CodeCheck = lo.Code,
                             Name = ll.Name,

                         }).ToList();

            if (list != null)
            {
                var ln = list.Where(x => x.CodeCheck == currentLanguage).FirstOrDefault();
                if (ln != null)
                {
                    languageDbName = ln.Name;
                }
                else
                {
                    languageDbName = "";
                }

            }
            else
            {
                languageDbName = "";
            }
            
            return list;
        }

        public List<LanguageView> LanguageList(string currentLanguage)
        {
            return (from li in db.Language
                        join ll in db.LanguageLanguage
                        on li.Id equals ll.LanguageCheckId
                        join lo in db.Language on ll.LanguageObjectId equals lo.Id
                        where li.Code == currentLanguage
                        select new LanguageView
                        {
                            Id = lo.Id,
                            CodeObject = li.Code,
                            CodeCheck = lo.Code,
                            Name = ll.Name,

                        }).ToList();
        }
    }

}