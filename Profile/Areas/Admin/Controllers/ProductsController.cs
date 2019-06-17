using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using CKFinder;
using System.Web.Script.Serialization;

namespace Profile.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        List<Product> listProduct = new List<Product>();
        // GET: Admin/Products
        [HttpGet]
        public ActionResult Index()
        {
            // var listProduct = new List<Product>();
            //listProduct.Add(new Product(){
            //    ID=1,
            //    Name="Tran Xuan Phi",
            //    MetaTitle ="phi",
            //    MetaKeywords= "phi",
            //    Price=12111414,
            //    Image="",
            //    Quantity=1213
            //});
            // listProduct.Add(new Product()
            // {
            //     ID = 2,
            //     Name = "Tran Xuan Phu",
            //     MetaTitle = "phu",
            //     MetaKeywords = "phu",
            //     Price = 6565656,
            //     Image = "",
            //     Quantity = 55555
            // });
            // return Json(new
            // {
            //     data = listProduct

            // }, JsonRequestBehavior.AllowGet);
            //var dao = new ProductsDao();
            //var model = dao.ListProducts();
            //return View(model);
            return View();
        }
        [HttpGet]
        public JsonResult GetProducts()
        {
            var dao = new ProductsDao();
            var listProduct = dao.ListProducts();

            return Json(new
            {
                data = listProduct,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new Product();
            model.CreatedDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.TopHot = DateTime.Now;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ProductsDao();
            var model = dao.GetById(id);
            return View(model);
        }

        [HttpGet]
        public JsonResult LoadJson()
        {
            var listProduct = new List<Product>();
            return Json(new
            {
                data = listProduct,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Detail(long id)
        {
            var dao = new ProductsDao();
            var listProduct = dao.GetDetail(id);
            return Json(new { data = listProduct, status = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product products)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductsDao();
                var id = dao.Insert(products);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    ModelState.AddModelError("", "Update not success!");
                }
            }
            return View();
        }
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public JsonResult Update(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Product products = serializer.Deserialize<Product>(model);
            var dao = new ProductsDao();
            var result = dao.Update(products);
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult Save(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Product products = serializer.Deserialize<Product>(model);
            var dao = new ProductsDao();
            bool status = false;
            string message = string.Empty;
            //Add new Products
            if (products.ID == 0)
            {
                try
                {
                    var result = dao.Insert(products);
                    status = true;
                }
                catch (Exception ex)
                {

                    status = false;
                    message = ex.Message;
                }

            }
            else
            {
                //Update Products
                try
                {
                    var entity = dao.Update(products);
                    status = true;
                }
                catch (Exception ex)
                {

                    status  = false;
                    message = ex.Message;
                }

            }
            return Json(new
            {
                status  = status,
                message = message
            });

        }
        [HttpDelete]
        public JsonResult Delete (long id)
        {
            
            var dao = new ProductsDao();
            bool result = dao.Delete(id);
            try
            {       
               
                return Json(new
                {
                    status = result 
                });
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = result,
                    message = ex.Message
                });
            }

        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Product entity)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductsDao();
                var result = dao.Update(entity);
                if (result)
                {
                    return RedirectToAction("Index", "Products");
                }
                else
                {
                    ModelState.AddModelError("", "Update not success!");
                }
            }
            return View();
        }

        //[HttpDelete]
        //public ActionResult Delete(long id)
        //{
        //    new ProductsDao().Delete(id);
        //    return RedirectToAction("Index");
        //}
    }
}