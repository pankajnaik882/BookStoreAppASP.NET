using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class WishlistManager : IWishlistManager
    {
        private IWishlistRepository wishlistRepository;

        public WishlistManager(IWishlistRepository wishlistRepository) 
        {
            this.wishlistRepository = wishlistRepository;
        }

        public WishModel AddWishlist(WishModel wishList, int userId)
        {
            return this.wishlistRepository.AddWishlist(wishList, userId);
        }

        public IEnumerable<WishModel> GetWishList(int userId)
        {
            return this.wishlistRepository.GetWishList(userId);
        }

        public string DeleteWishList(int bookID, int userID)
        {
            return this.wishlistRepository.DeleteWishList(bookID, userID);
        }
    }
}
