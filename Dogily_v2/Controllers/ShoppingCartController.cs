using Dogily_v2.Models;
using Dogily_v2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dogily_v2.Controllers
{
    public class ShoppingCartController : Controller
    {
        DogilyEntities6 db = new DogilyEntities6();
        // GET: ShoppingCart
        public ActionResult EmptyCart()
        {
            return View();
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("EmptyCart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(string IDPro)
        {
            var pro = db.Products.SingleOrDefault(p => p.IDPro == IDPro);
            if (pro != null)
            {
                GetCart().AddProductToCart(pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult UpdateCartAmount(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;

            string IDPro = form["IDPro"];
            int amount = int.Parse(form["proAmount"]);
            cart.UpdateAmount(IDPro, amount);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult RemoveCart(string IDPro)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveCartItem(IDPro);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult BagCart()
        {
            int totalItemAmount = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
            {
                totalItemAmount = cart.TotalAmount();
            }
            ViewBag.TotalAmount = totalItemAmount;
            return View("BagCart");
        }
        public ActionResult Order()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("EmptyCart");
            }
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order order = new Order();
                order.OrderDate = DateTime.Now;
                order.CusName = form["CusName"];
                order.CusPhone = form["CusPhone"];
                order.CusEmail = form["CusEmail"];
                order.Adress = form["Adress"];
                order.Note = form["Note"];
                order.TotalPrice = (double)cart.TotalPrice();
                db.Orders.Add(order);
                foreach (var item in cart.Items)
                {
                    OrderDetail detail = new OrderDetail();
                    detail.IDOrder = order.IDOrder;
                    detail.IDPro = item.Product.IDPro;
                    detail.Amount = (byte)item.Amount;
                    detail.TotalPrice = item.Product.Price;
                    db.OrderDetails.Add(detail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOutSuccessfully", "ShoppingCart");
            }
            catch
            {
                return Content("Error");
            }
        }
        public ActionResult CheckOutSuccessfully()
        {
            return View();
        }
    }
}