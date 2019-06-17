using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TemplatePersonal.Areas.Admin.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "Mời bạn nhập user name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời bạn nhập pass word")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}