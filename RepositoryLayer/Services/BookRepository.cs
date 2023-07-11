using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Net;

namespace RepositoryLayer.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly IConfiguration config;
        public readonly string connectionString;

        public BookRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("BookStoreDB");
            this.config = config;

        }

        public BookModel AddBook(BookModel book)
        {

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Add_Book", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookName", book.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);                
                cmd.Parameters.AddWithValue("@Description", book.Description);
                cmd.Parameters.AddWithValue("@BookImage", book.BookImage);
                cmd.Parameters.AddWithValue("@InStock", book.InStock);
                cmd.Parameters.AddWithValue("@Price", book.Price);
                cmd.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("@TotalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                cmd.Parameters.AddWithValue("@CreatedAt", book.CreatedAt);
                cmd.Parameters.AddWithValue("@UpdatedAt", book.UpdatedAt);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return book;
        }

        public IEnumerable<BookModel> GetAllBooks()
        {

            List<BookModel> bookmodellist = new List<BookModel>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_GetAllBooks_Book", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BookModel bookModel = new BookModel()
                        {
                            
                            BookID = Convert.ToInt32(reader["BookID"]),
                            BookName = reader["BookName"].ToString(),
                            AuthorName = reader["AuthorName"].ToString(),
                            Description= reader["Description"].ToString(),
                            BookImage = reader["BookImage"].ToString(),
                            InStock = Convert.ToInt32(reader["InStock"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]),
                            TotalRating = Convert.ToInt32(reader["TotalRating"]),
                            RatingCount = Convert.ToInt32(reader["RatingCount"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"])

                        };

                        bookmodellist.Add(bookModel);
                    }
                }
                con.Close();
            }
            return bookmodellist;
        }


        public string UpdateBook(BookModel book)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Update_Book", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", book.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", book.AuthorName);
                    cmd.Parameters.AddWithValue("@Description", book.Description);
                    cmd.Parameters.AddWithValue("@BookImage", book.BookImage);
                    cmd.Parameters.AddWithValue("@InStock", book.InStock);
                    cmd.Parameters.AddWithValue("@Price", book.Price);
                    cmd.Parameters.AddWithValue("@DiscountPrice", book.DiscountPrice);
                    cmd.Parameters.AddWithValue("@TotalRating", book.TotalRating);
                    cmd.Parameters.AddWithValue("@RatingCount", book.RatingCount);
                    cmd.Parameters.AddWithValue("@CreatedAt", book.CreatedAt);
                    cmd.Parameters.AddWithValue("@UpdatedAt", book.UpdatedAt);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            book.BookID = Convert.ToInt32(reader["BookID"]);
                            book.BookName = reader["BookName"].ToString();
                            book.AuthorName = reader["AuthorName"].ToString();
                            book.Description = reader["Description"].ToString();
                            book.BookImage = reader["BookImage"].ToString();
                            book.InStock = Convert.ToInt32(reader["InStock"]);
                            book.Price = Convert.ToInt32(reader["Price"]);
                            book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            book.TotalRating = Convert.ToInt32(reader["TotalRating"]);
                            book.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                            book.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
                            book.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);

                        }
                    }
                    con.Close();
                }
                return "Book Updated Successfully";
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string DeleteLabel(string BookName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Delete_Book", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", BookName);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Book Deleted Successfully";
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
