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
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IConfiguration config;
        public readonly string connectionString;

        public WishlistRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("BookStoreDB");
            this.config = config;

        }

        public WishModel AddWishlist(WishModel wishList , int userId)
        {

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Add_WishList", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@BookID", wishList.BookID);
                cmd.Parameters.AddWithValue("@CreatedAt", wishList.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", wishList.UpdatedAt);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return wishList;
        }

        public IEnumerable<WishModel> GetWishList(int userId)
        {

            List<WishModel> wishmodellist = new List<WishModel>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_GetAll_WishList", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        WishModel wishModel = new WishModel()
                        {

                            
                            WishListID= Convert.ToInt32(reader["WishListID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            BookID = Convert.ToInt32(reader["BookID"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])

                        };

                        wishmodellist.Add(wishModel);
                    }
                }
                con.Close();
            }
            return wishmodellist;
        }

        public string DeleteWishList(int bookID , int userID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Delete_WishList", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookID", bookID);
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Book Deleted Successfully from WishList";
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
