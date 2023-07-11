using CommonLayer;
using ManagerLayer.Interface;
using MassTransit.Pipeline.Pipes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartManager cartManager;

        public CartController(ICartManager cartManager)
        {
            this.cartManager = cartManager;

        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddCart")]
        
        public IActionResult AddCart(CartModel cart )
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                CartModel cartAddData = this.cartManager.AddCart(cart,UserID);
                if(cartAddData != null )
                {
                    return this.Ok(new { success = true, message = "Book Added To Cart Successfully", result = cartAddData });
                }

                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch 
            {
                return this.NotFound(new { success = false });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAllCart")]
        public IActionResult GetAllCart()
        {

            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                List<CartModel> cartModelList = (List<CartModel>)this.cartManager.GetCart(UserID);

                if (cartModelList != null)
                {
                    return this.Ok(new { success = true, message = "CartList Fetched Successfully", result = cartModelList });
                }
                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new {success = false , message = ex.Message });
            }    
        }

        [Authorize(Roles = Role.User)]
        [HttpPut]
        [Route("UpdateCart")]
        public IActionResult UpdateCart(CartModel cart)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                CartModel cartUpdateData = this.cartManager.UpdateCart(UserID, cart);
                if (cartUpdateData != null)
                {
                    return this.Ok(new { success = true, message = "Cart Updated Successfully", result = cartUpdateData });
                }
                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new {success = true, message = ex.Message});
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete]
        [Route("DeleteFromCart")]
        public IActionResult DeleteCart(int bookID)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                string deleteCartData = this.cartManager.DeleteCart(UserID, bookID);

                if (deleteCartData != null)
                {
                    return this.Ok(new { Success = true, message = "Deleted Book from Cart successfully", result = deleteCartData });
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
