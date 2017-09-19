using Company.Extends.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Areas.Manage.Models
{
    public class SProductLanguage
    {
        public int Id { get; set; }
        public int SProductId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Describe { get; set; }
        public string FuncTitle { get; set; }
        public string FuncDescribe { get; set; }
        public string FuncList1 { get; set; }
        public string FuncList2 { get; set; }
        public string FuncList3 { get; set; }
        public string FuncList4 { get; set; }
        public string FuncList5 { get; set; }
        public string FuncList6 { get; set; }
        public string FuncList7 { get; set; }
        public string FuncList8 { get; set; }
        public string FuncList9 { get; set; }
        public string FuncList10 { get; set; }
        public string CustomerTitle { get; set; }
        public string CustomerDescribe { get; set; }
        public string CustomerImage1 { get; set; }
        public string CustomerImage2 { get; set; }
        public string CustomerImage3 { get; set; }
        public string CustomerImage4 { get; set; }
        public string CustomerImage5 { get; set; }
        public string CustomerImage6 { get; set; }

    }
}