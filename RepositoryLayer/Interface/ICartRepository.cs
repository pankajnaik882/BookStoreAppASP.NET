using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRepository
    {
        public CartModel AddCart(CartModel cart, int userID);

        public IEnumerable<CartModel> GetCart(int userId);

        public string DeleteCart(int bookID, int userID);

        public CartModel UpdateCart(int userID, CartModel cart);
    }
}
