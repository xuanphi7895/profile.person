using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
 

namespace Profile.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        [HttpGet]
        public ActionResult Index()
        {
            var dao = new CategoryDao();
            var model = dao.ListCategories();
            return View(model);
        }
        [HttpGet]
       
        public ActionResult Create()
        {
            var model = new Category();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var  dao = new CategoryDao();
            var model = dao.GetById(id);
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                var id = dao.Insert(category);
                if (id > 0)
                {
                    return RedirectToAction("Index","Category");
                }
                else
                {
                    ModelState.AddModelError("","Not add category success!");
                }

            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category categories)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDao();
                bool result = dao.Update(categories);
                if (result)
                {
                    return RedirectToAction("Index","Category");
                }
                else
                {
                     ModelState.AddModelError("","Update not success!");
                }
            }
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}