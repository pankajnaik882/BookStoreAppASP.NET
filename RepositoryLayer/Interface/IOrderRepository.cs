using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRepository
    {
        public OrderModel AddOrder(OrderModel orderModel, int userID);

        public IEnumerable<OrderModel> GetOrder(int userId);

        public string DeleteOrder(int bookID, int userID);
    }
}
