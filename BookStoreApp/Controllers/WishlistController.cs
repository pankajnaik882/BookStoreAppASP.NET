using CommonLayer;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using System.Collections.Generic;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private IWishlistManager wishlistManager;

        public WishlistController(IWishlistManager wishlistManager) 
        {
            this.wishlistManager = wishlistManager;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddWishList")]
        public IActionResult AddWishList(WishModel wishlistAddingModel )

        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                WishModel wishAddData = this.wishlistManager.AddWishlist(wishlistAddingModel, UserID);

                if (wishAddData != null)
                {
                    return this.Ok(new { Success = true, message = " Book Added to WishList Successfully", result = wishAddData });
                }

                return this.BadRequest(new { success = true, message = "Book Already Exists in WishList" });

            }
            catch (Exception)
            {
                return this.NotFound(new { success = false });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetWishList")]
        public IActionResult GetWishList()
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                List<WishModel> Wishmodellist = (List<WishModel>)this.wishlistManager.GetWishList(UserID);

                if (Wishmodellist != null)
                {
                    return this.Ok(new { Success = true, message = "WishList Fetched Successfully", result = Wishmodellist });
                }
                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete]
        [Route("DeleteFromWishList")]
        public IActionResult DeleteBook(int bookID)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                string deleteWishList = this.wishlistManager.DeleteWishList(bookID, UserID);


                if (deleteWishList != null)
                {
                    return this.Ok(new { Success = true, message = "Deleted Book from Wishlist successfully", result = deleteWishList });
                }


                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
