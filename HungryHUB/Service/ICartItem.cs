using HungryHUB.Entity;
using System.Collections.Generic;

namespace HungryHUB.Service
{
    public interface ICartItemService
    {
        void AddCartItem(CartItem cartItem);
        List<CartItem> GetAllCartItems();
        CartItem GetCartItemByName(string cartItemName);
        List<CartItem> GetCartItemsByUser(string username);
        void RemoveCartItem(string cartItemName);
        void UpdateCartItem(string cartItemName, CartItem updatedCartItem);
    }
}
