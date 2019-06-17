using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

namespace Profile.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        [HttpGet]
        public ActionResult Index()
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListProductCategory();
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductCategory();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            SetViewBag();
            return View(model);
        }
        public void SetViewBag(long? selectedID = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.ParentID = new SelectList(dao.ListProductCategory(), "ID", "Name", selectedID);
            //return dao.ListProductCategory().Where(x=>x.Status==true).ToList();
           
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ProductCategoryDao();
            var model = dao.GetById(id);
            SetViewBag(model.ID);
            //SetViewBag();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var id = dao.Insert(productcategory);
                if (id > 0)
                {
                    return RedirectToAction("Index","ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("","Create not success!");
                }
            }
            return View();

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(productcategory);
                if (result)
                {
                    return RedirectToAction("Index","ProductCategory");
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
            new ProductCategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
    }
}