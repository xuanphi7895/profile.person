using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profile.Areas.Admin.Controllers;
using Model.EF;
using Model.DAO;
using Profile.Common;
using Facebook;
using System.Configuration;


namespace Profile.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //GET Account/Login
        public ActionResult Login()
        {
            return View();
        }
        

        //public ActionResult LoginFacebook()
        //{
        //    var fb = new FacebookClient();
        //    var loginUrl = fb.GetLoginUrl(
        //        new { clien_id = ConfigurationManager.AppSettings["FbAppId"],
        //               client_serect = ConfigurationManager.AppSettings["FbAppSerect"].
        //               redirect_uri = RedirectUri
        //        });
        //}


        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UsersDao();
                var result = dao.Login(user.UserName, Encryptor.MD5Hash(user.Password) );
                if (result ==1 )
                {
                    var usr = dao.UserGetById(user.UserName);
                    var userSession = new Common.UserLogin();
                    userSession.UserName = usr.UserName;
                    userSession.UserID = usr.ID;
                    Session.Add(Common.CommonConstant.User_Session, userSession);
                    return RedirectToAction("Index","Users");

                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Username or Password is incorrect!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("","Account is locked!");
                }
                else
                {
                    ModelState.AddModelError("", "Account is not sign up!");
                }
            }
            return View("Login");
            
        }
    }
}