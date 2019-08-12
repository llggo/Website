using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sales.Areas.Manage.Models
{
    public class OptionModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Logo { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Infomation { get; set; }

        public string Facbook { get; set; }
        public string Youtube { get; set; }
        public string Twitter { get; set; }
        public string Google { get; set; }

        public Sales.Models.Modified Edit { get; set; }
        [Display(Name = "Tạo")]
        public Sales.Models.Modified Create { get; set; }
    }
}