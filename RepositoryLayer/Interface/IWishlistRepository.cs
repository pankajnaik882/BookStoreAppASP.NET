using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishlistRepository
    {
        public WishModel AddWishlist(WishModel wishList, int userId);

        public IEnumerable<WishModel> GetWishList(int userId);

        public string DeleteWishList(int bookID, int userID);
    }
}
