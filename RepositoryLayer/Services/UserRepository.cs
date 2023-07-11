using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration config;
        public readonly string connectionString;

        public UserRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("BookStoreDB");
            this.config = config;

        }

        //-----------------UserModel For Adding Users to Database-----------//
        public UserModel AddUsers(UserModel users)
        {

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Insert_Users", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", users.FullName);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Password", EncryptPasswordBase64(users.Password));
                cmd.Parameters.AddWithValue("@PhoneNumber", users.PhoneNumber);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return users;
        }

        //-------------Base64-Encryption Decryption Method----------//

        public static string EncryptPasswordBase64(String text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);

        }

        public static string DecryptPasswordBase64(String base64EncodeData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodeData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        //--------------------------Login---------------------------------------//

        public UserModel Login(UserLogin login)
        {
            UserModel userModel = new UserModel();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Login_Users", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", login.Email);
                cmd.Parameters.AddWithValue("@Password", EncryptPasswordBase64(login.Password));

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userModel.UserID = reader.IsDBNull("userID") ? 0 : reader.GetInt32("userID");
                        userModel.FullName = reader.GetString(1);
                        userModel.Email = reader.GetString(2);
                        userModel.PhoneNumber = reader.GetInt64(4);

                    }
                }
                con.Close();
            }
            return userModel;
        }

        //-------------------------JWT--------------------------------
        public string GenerateJWToken(string Email, int UserID)
        {

            try
            {
                var LoginsecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Jwt:Key"]));
                var Credentials = new SigningCredentials(LoginsecurityKey, SecurityAlgorithms.HmacSha256);

                var Claims = new[]
                {
                       new Claim (ClaimTypes.Role,"User"),
                       new Claim( "Email", Email),
                       new Claim("UserID", UserID.ToString()),
                };

                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: Credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //------------------------Forget password ---------------------

        public UserModel ForgetPassword(UserForgetPassword forgetPassword)
        {
            UserModel userModel = new UserModel();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_ForgetPassword_Users", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", forgetPassword.Email);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userModel.UserID = reader.IsDBNull("userID") ? 0 : reader.GetInt32("userID");
                        userModel.FullName = reader.GetString(1);                        
                        userModel.Email = reader.GetString(2);
                        userModel.Password = DecryptPasswordBase64(reader.GetString(3));
                        userModel.PhoneNumber = reader.GetInt64(4);


                    }
                }
                con.Close();
            }
            return userModel;
        }

        public bool ResetPassword(UserResetPassword userResetPassword, string Email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {

                    SqlCommand cmd = new SqlCommand("dbo.usp_ResetPassword_Users", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", EncryptPasswordBase64(userResetPassword.Password));

                    con.Open();

                    int ResetOrNot = cmd.ExecuteNonQuery();

                    if (ResetOrNot >= 1)
                    {
                        return true;
                    }

                    con.Close();

                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
