using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profile.Common;

namespace Profile.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// CheckAuthation 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (Common.UserLogin)Session[Common.CommonConstant.User_Session];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login", Area = "Admin" }));
            }

            base.OnActionExecuting(filterContext);
        }
        /// <summary>
        /// Set Alert
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success"; 
            }
            else if (type == "info")
            {
                TempData["AlertType"] = "alert-info";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }

        }
    }
}