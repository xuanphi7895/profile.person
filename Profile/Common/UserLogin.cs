using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Profile.Areas.Admin.Common
{
    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}