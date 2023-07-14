using CommonLayer;
using ManagerLayer.Interface;
using MassTransit.Audit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager) 
        {
            this.orderManager = orderManager;
        }

        [Authorize(Roles =Role.User)]
        [HttpPost]
        [Route("AddOrder")]
        
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                OrderModel orderModelData = this.orderManager.AddOrder(orderModel,UserID);

                if (orderModelData != null)
                {
                    return this.Ok(new { success = true, message = "Order Added To Cart Successfully", result = orderModelData });
                }
                return this.BadRequest(new { success = true, message = "Process Failed" });
            }

            catch (Exception ex) 
            {
                return this.NotFound(new {success = false , message = ex.Message});
                
            }

        }

        [Authorize(Roles =Role.User)]
        [HttpGet]
        [Route("GetAllOrders")]

        public IActionResult GetAllOrder()
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                List<OrderModel> ordermodelData = (List<OrderModel>)this.orderManager.GetOrder(UserID);

                if (ordermodelData != null)
                {
                    return this.Ok(new { success = true, message = "Orders Fetched Successfully", result = ordermodelData });
                }

                return this.BadRequest(new { success = true, message = "Process Failed" });

            }
            catch (Exception ex)
            {
                return this.NotFound(new {success = false , message = ex.Message});
            }
        }

        [Authorize(Roles =Role.User)]
        [HttpDelete]
        [Route("DeleteOrder")]

        public IActionResult DeleteOrder(int BookID)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                string deleteOrderData = this.orderManager.DeleteOrder(BookID, UserID);

                if (deleteOrderData != null)
                {
                    return this.Ok(new { Success = true, message = "Deleted Order successfully", result = deleteOrderData });
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
