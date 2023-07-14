using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public OrderRepository(IConfiguration config ) 
        {
            this.config = config;
            this.connectionString = config.GetConnectionString("BookStoreDB");
        }

        public OrderModel AddOrder(OrderModel orderModel, int userID )
        {
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Add_Order", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID",userID);
                cmd.Parameters.AddWithValue("@BookID", orderModel.BookID);
                cmd.Parameters.AddWithValue("@AddressID", orderModel.AddressID);
                cmd.Parameters.AddWithValue("@Quantity", orderModel.Quantity);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return orderModel;
        }

        public IEnumerable<OrderModel> GetOrder(int userId)
        {

            List<OrderModel> ordermodellist = new List<OrderModel>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Get_Order", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModel orderModel = new OrderModel()
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            AddressID = Convert.ToInt32(reader["AddressID"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            Quantity = Convert.ToInt32(reader["Quantity"]),

                        };
                        ordermodellist.Add(orderModel);
                    }
                }
                con.Close();
            }
            return ordermodellist;
        }

        public string DeleteOrder(int bookID, int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Delete_Order", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Order Deleted Successfully";
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
