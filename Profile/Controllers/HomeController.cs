using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;


namespace Profile.Controllers
{
    public class HomeController : Controller
    {
       
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult MenuNavigation(Menu id)
        {
            var dao = new MenuDao().ListMenu(id);
            return PartialView(dao);
        }
        [ChildActionOnly]
        public ActionResult Slide(Slide id)
        {
            List<Slide> dao = new SlideDao().GetListSlide();
            return View(dao);
        } 
        [HttpGet]
        public JsonResult LoadJson()
        {
            var listJson = new List<Menu>();
            listJson.Add(new Menu() {
                ID=1,
                TypeID =2,
                Link = "phi",
                Target="_blank",
                Status = true,
                DisplayOrder=1
            });
            listJson.Add(new Menu()
            {
                ID = 2,
                TypeID = 3,
                Link = "Quy",
                Target = "_blank",
                Status = true,
                DisplayOrder = 1
            });
            listJson.Add(new Menu()
            {
                ID = 4,
                TypeID = 4,
                Link = "Phu",
                Target = "_blank",
                Status = true,
                DisplayOrder = 1
            });
            return Json(new
            {
                data = listJson,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

    }
}   