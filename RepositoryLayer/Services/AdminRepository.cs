using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RepositoryLayer.Services
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IConfiguration config;
        public readonly string connectionString;

        public AdminRepository(IConfiguration config)
        {
            this.connectionString = config.GetConnectionString("BookStoreDB");
            this.config = config;

        }

        public string GenerateJWToken(string Email, int AdminID)
        {
            try
            {
                var LoginsecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["Jwt:Key"]));
                var Credentials = new SigningCredentials(LoginsecurityKey, SecurityAlgorithms.HmacSha256);

                var Claims = new[]
                {       
                       new Claim(ClaimTypes.Role,"Admin"),
                       new Claim( "Email", Email),
                       new Claim("AdminID", AdminID.ToString()),
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

        public AdminModel LoginAdmin(string AdminEmail , string Password)
        {
            AdminModel adminModel = new AdminModel();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Login_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", AdminEmail);
                cmd.Parameters.AddWithValue("@Password", EncryptPasswordBase64(Password));

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        adminModel.AdminID = reader.IsDBNull("AdminID") ? 0 : reader.GetInt32("AdminID");
                        adminModel.Email = reader.GetString(1);
                        adminModel.PhoneNumber = reader.GetInt64(3);
                    }
                }
                con.Close();
            }
            return adminModel;
        }

        public static string EncryptPasswordBase64(String text)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);

        }

       
    }
}
