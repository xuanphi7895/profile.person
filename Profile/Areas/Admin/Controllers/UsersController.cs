using Model.EF;
using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profile.Common;
using PagedList;

namespace Profile.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        // GET: Admin/Users
        //Use PagedList
        //public ActionResult Index(int page=1, int pageSize=10)
        //{
        //    var dao = new UsersDao();
        //    var model = dao.ListUsers(page,pageSize);
        //    return View(model);      
        //}
        [HttpGet]
        public ActionResult Index()
        {
            var dao = new UsersDao();
            var model = dao.ListUsers();
            return View(model);
        }
        //Get Admin/Users
        [HttpGet]
        public ActionResult Create()
        {
            return View(new User());
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new UsersDao();
            var model = dao.UserGetById(id);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
               var dao = new UsersDao();
               
                var encrytedMD5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encrytedMD5Pass;
                var id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Add users success!","success");
                    return RedirectToAction("Index","Users");
                }
                else
                {
                    ModelState.AddModelError("","Additional user!");
                }
            }
            return View("Index");
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UsersDao();
                var encrytedMD5Pass = Encryptor.MD5Hash(user.Password);
                user.Password = encrytedMD5Pass;
                var result = dao.Update(user);
                if (result)
                {
                    SetAlert("Edit users success!", "success");
                    return RedirectToAction("Index", "Users");
                }
                else
                {

                    ModelState.AddModelError("","Update not susccess!");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new UsersDao().Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangStatus(long id)
        {
            var result = new UsersDao().ChangStatus(id);
            return Json(new {
                status = result
            });
        }
    }
}