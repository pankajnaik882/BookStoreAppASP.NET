using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IWishlistManager
    {

        public WishModel AddWishlist(WishModel wishList, int userId);

        public IEnumerable<WishModel> GetWishList(int userId);

        public string DeleteWishList(int bookID, int userID);
    }
}
