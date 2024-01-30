using HungryHUB.Database;
using HungryHUB.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HungryHUB.Service
{
    public class CartItemService : ICartItemService
    {
        private readonly MyContext _context;

        public CartItemService(MyContext context)
        {
            _context = context;
        }

        public void AddCartItem(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return _context.CartItems.ToList();
        }

        public CartItem GetCartItemByName(string cartItemName)
        {
            return _context.CartItems.FirstOrDefault(item => item.MenuItem.Name == cartItemName);
        }

        public List<CartItem> GetCartItemsByUser(string username)
        {
            return _context.CartItems.Where(item => item.User.Name == username).ToList();
        }

        public void RemoveCartItem(string cartItemName)
        {
            var itemToRemove = _context.CartItems.FirstOrDefault(item => item.MenuItem.Name == cartItemName);
            if (itemToRemove != null)
            {
                _context.CartItems.Remove(itemToRemove);
                _context.SaveChanges();
            }
        }

        public void UpdateCartItem(string cartItemName, CartItem updatedCartItem)
        {
            var existingItem = _context.CartItems.FirstOrDefault(item => item.MenuItem.Name == cartItemName);
            if (existingItem != null)
            {
                // Update properties based on your requirements
                existingItem.Quantity = updatedCartItem.Quantity;
                _context.SaveChanges();
            }
        }
    }
}
