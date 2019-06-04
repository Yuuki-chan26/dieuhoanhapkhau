using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dieuhoanhapkhau.web.Models
{
    public class Carts
    {
        public List<CartItem> ListItem { get; set; }

        public Carts()
        {
            ListItem = new List<CartItem>();
        }
        public void AddToCart(CartItem item)
        {

            if (ListItem.Where(x => x.ProductName.Equals(item.ProductName)).Any())
            {
                var myItem = ListItem.Single(x => x.ProductName.Equals(item.ProductName));
                myItem.Quantity += item.Quantity;
                myItem.Total += item.Quantity * item.Price;
            }
            else
            {
                ListItem.Add(item);
            }
        }
        public bool RemoveFromCart(int proId)
        {
            CartItem itemExists = ListItem.Where(x => x.ProductId == proId).SingleOrDefault();
            if (itemExists != null)
            {
                ListItem.Remove(itemExists);
            }
            return true;
        }
        public bool UpdateCart(int Id, int quanlity)
        {
            CartItem itemExists = ListItem.Where(x => x.ProductId == Id).SingleOrDefault();
            if (itemExists != null)
            {
                itemExists.Quantity = quanlity;
                itemExists.Total = itemExists.Quantity * itemExists.Price;
            }
            return true;
        }
        public bool EmtyCart()
        {
            ListItem.Clear();
            return true;
        }
        public bool ContainsItem(int id)
        {
            if (ListItem.Where(x => x.ProductId == id).Any())
            {
                return true;
            }
            return false;
        }
    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ShortName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
    public class ShoppingCartModels
    {
        public Carts Cart { get; set; }
    }
    
}