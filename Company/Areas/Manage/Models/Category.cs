﻿using Company.Extends.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Company.Areas.Manage.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_order")]
        public int Order { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_visible")]
        public bool Visible { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_enable")]
        public bool Enable { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_create_user")]
        public string CreateUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_create_time")]
        public DateTime CreateTime { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_user")]
        public string LastEditUser { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "website_last_edit_time")]
        public DateTime LastEditTime { get; set; }
    }

    public class CrudCategory
    {
        public Category Category { get; set; }
        public Dictionary<string, CategoryLanguage> CategoryLanguage { get; set; }
        public Language Language { get; set; }
    }
}