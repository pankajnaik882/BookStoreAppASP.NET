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
    public class CartRepository : ICartRepository
    {
        private readonly IConfiguration config;
        public readonly string connectionString;

        public CartRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("BookStoreDB");
            this.config = config;

        }

        public CartModel AddCart(CartModel cart , int userID)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Add_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@BookID", cart.BookID);
                cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("@CreatedAt", cart.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", cart.UpdatedAt);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return cart;
        }

        public IEnumerable<CartModel> GetCart(int userId)
        {

            List<CartModel> cartmodellist = new List<CartModel>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_GetAll_Cart", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CartModel cartModel = new CartModel()
                        {
                            CartID = Convert.ToInt32(reader["CartID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            BookID = Convert.ToInt32(reader["BookID"]),
                            Quantity= Convert.ToInt32(reader["Quantity"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])

                        };

                        cartmodellist.Add(cartModel);
                    }
                }
                con.Close();
            }
            return cartmodellist;
        }

        public CartModel UpdateCart(int userID, CartModel cart)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Update_Cart", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookID",cart.BookID);
                    cmd.Parameters.AddWithValue("@UserID", userID );
                    cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cart.CartID = reader.GetInt32(0);
                            cart.BookID = reader.GetInt32(1);
                            cart.UserID = reader.GetInt32(2);
                            cart.Quantity = reader.GetInt32(3);
                            cart.CreatedAt = reader.GetDateTime(4);
                            cart.UpdatedAt = reader.GetDateTime(5); 

                        }
                    }
                    con.Close();
                }
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteCart(int bookID, int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Delete_Cart", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Book Deleted Successfully from Cart";
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
