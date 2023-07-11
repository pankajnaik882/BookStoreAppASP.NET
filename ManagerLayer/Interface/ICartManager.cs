using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface ICartManager
    {
        public CartModel AddCart(CartModel cart, int userID);

        public IEnumerable<CartModel> GetCart(int userId);

        public string DeleteCart(int bookID, int userID);

        public CartModel UpdateCart(int userID, CartModel cart);
    }
}
