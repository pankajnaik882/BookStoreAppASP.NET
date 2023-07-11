using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class CartManager : ICartManager
    {
        private ICartRepository cartRepository;

        public CartManager(ICartRepository cartRepository) 
        {
            this.cartRepository = cartRepository;
        }

        public CartModel AddCart(CartModel cart, int userID)
        {
            return this.cartRepository.AddCart(cart, userID);  
        }

        public IEnumerable<CartModel> GetCart(int userId)
        {
            return this.cartRepository.GetCart(userId);
        }

        public string DeleteCart(int bookID, int userID)
        {
            return this.cartRepository.DeleteCart(bookID, userID);
        }

        public CartModel UpdateCart(int userID,  CartModel cart)
        {
            return this.cartRepository.UpdateCart(userID, cart);   
        }
    }
}
