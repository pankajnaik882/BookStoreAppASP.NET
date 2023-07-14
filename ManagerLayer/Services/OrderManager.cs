using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderRepository orderRepository;

        public OrderManager(IOrderRepository orderRepository) 
        {
            this.orderRepository = orderRepository;      
        }

        public OrderModel AddOrder(OrderModel orderModel, int userID)
        {
            return this.orderRepository.AddOrder(orderModel, userID);
        }

        public IEnumerable<OrderModel> GetOrder(int userId)
        {
            return this.orderRepository.GetOrder(userId);
        }

        public string DeleteOrder(int bookID, int userID)
        {
            return this.orderRepository.DeleteOrder(bookID, userID);
        }
    }
}
