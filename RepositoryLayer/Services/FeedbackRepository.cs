using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly string connectionString;
        private readonly IConfiguration config;

        public FeedbackRepository(IConfiguration config) 
        {
            this.connectionString = config.GetConnectionString("BookStoreDB"); 
            this.config = config;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedback , int UserID)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Add_Feedback", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID",UserID);
                cmd.Parameters.AddWithValue("@BookID",feedback.BookID);
                cmd.Parameters.AddWithValue("@Comment",feedback.Comment);
                cmd.Parameters.AddWithValue("@Review",feedback.Review);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
            return feedback;
        }

        public IEnumerable<FeedbackModel> GetFeedbacks(int BookID) 
        {
            List<FeedbackModel> feedbackList = new List<FeedbackModel>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_Get_Feedback", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookID", BookID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeedbackModel feedback = new FeedbackModel()
                        {
                            FeedbackID = reader.GetInt32(0),
                            UserID = reader.GetInt32(1),
                            BookID = reader.GetInt32(2),
                            Review = reader.GetString(3),
                            Comment = reader.GetString(4)
                            
                        };

                        feedbackList.Add(feedback);
                    }
                }
                con.Close();
            }
            return feedbackList;
        }
    }
}
