using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.EF;
namespace Profile.Models
{
    public class CartItems
    {
        public Product Product { set; get; }
        public int Quantity { set; get; }
    }
}