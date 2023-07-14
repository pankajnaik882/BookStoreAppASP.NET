using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IOrderManager
    {
        public OrderModel AddOrder(OrderModel orderModel, int userID);

        public IEnumerable<OrderModel> GetOrder(int userId);

        public string DeleteOrder(int bookID, int userID);
    }
}
