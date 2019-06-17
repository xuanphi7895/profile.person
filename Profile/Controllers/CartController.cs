using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Profile.Models;
using Model.EF;
using Model.DAO;
namespace Profile.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list =new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return View();
        }
        public ActionResult AddItem(long productsId, int quantity)
        {
            var product = new ProductsDao().GetDetail(productsId);
            var cart = Session[CartSession];
            if (cart != null)
            {
                //TH Cart có Items
                var list = (List<CartItems>)cart;
                if (list.Exists(x => x.Product.ID == productsId))
                {
                    //Lặp qua list (list ở đây là mảng Object: Nên use foreach)
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productsId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                //Nếu list chưa có productId thì Add new 
                else
                {
                    var item = new CartItems();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                // gán vào Session
                Session[CartSession] = list;

            }
            else
            {
                //Add new object cart item. TH Cart chưa có Items. 
                var item = new CartItems();
                item.Product  = product;
                item.Quantity = quantity;
                var list =new List<CartItems>();
                // Gán list vào Session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

    }
}