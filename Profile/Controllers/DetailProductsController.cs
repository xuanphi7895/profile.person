using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;

namespace Profile.Controllers
{
    public class DetailProductsController : Controller
    {
        // GET: DetailProducts
        [HttpGet]
        public ActionResult Single()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Detail()
        {
            var dao = new ProductsDao();
            var list = dao.ListProducts();
            return View(list);
        }
        //[HttpGet]
        //public ActionResult Detail( long id)
        //{
        //    var dao = new ProductsDao().GetDetail(id);
        //    return View(dao);
        //}
    }
}