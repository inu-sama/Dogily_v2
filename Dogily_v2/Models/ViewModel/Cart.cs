using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dogily_v2.Models.ViewModel
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Amount { get; set; }
    }

    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        List<City> cities = new List<City>();
        List<District> districts = new List<District>();
        public IEnumerable<CartItem> Items { get {  return items; } }
        public IEnumerable<City> Cities { get {  return cities; } }
        public void AddProductToCart(Product product, int amount = 1)
        {
            var item = Items.FirstOrDefault(i => i.Product.IDPro == product.IDPro);
            if (item == null)
                items.Add(new CartItem
                {
                    Product = product,
                    Amount = amount
                });
            else
                item.Amount += amount;
        }
        public int TotalAmount()
        {
            return items.Sum(i => i.Amount);
        }
        public decimal TotalPrice()
        {
            var total = items.Sum(i => i.Amount * i.Product.Price);
            return (decimal)total;
        }
        public void UpdateAmount(string IDPro, int newAmount)
        {
            var item = items.Find(i => i.Product.IDPro == IDPro);
            if (item != null)
            {
                item.Amount = newAmount;
            }
        }
        public void RemoveCartItem(string IDPro)
        {
            items.RemoveAll(s => s.Product.IDPro == IDPro);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
}