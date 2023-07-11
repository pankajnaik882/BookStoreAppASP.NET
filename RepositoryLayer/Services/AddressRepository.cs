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
    public class AddressRepository : IAddressRepository
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public AddressRepository(IConfiguration config) 
        {
            this.config = config;
            this.connectionString = config.GetConnectionString("BookStoreDB");
        }

        public AddressModel AddAddress(AddressModel address, int userID , string Type)
        {
            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_Add_AddressDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@Address", address.Address);
                cmd.Parameters.AddWithValue("@City", address.City);
                cmd.Parameters.AddWithValue("@State", address.State);
                cmd.Parameters.AddWithValue("@TypeName", Type);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return address;
        }

        public IEnumerable<AddressModel> GetAddress(int userId)
        {

            List<AddressModel> addressmodellist = new List<AddressModel>();

            using (SqlConnection con = new SqlConnection(this.connectionString))
            {
                SqlCommand cmd = new SqlCommand("dbo.usp_GetAll_AddressDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", userId);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AddressModel addressModel = new AddressModel()
                        {
                            AddressID = Convert.ToInt32(reader["AddressID"]),
                            FullName = Convert.ToString(reader["FullName"]),
                            PhoneNumber = Convert.ToInt64(reader["PhoneNumber"]),
                            Address = Convert.ToString(reader["Address"]),
                            City = Convert.ToString(reader["City"]),
                            State = Convert.ToString(reader["State"]),
                            TypeID = Convert.ToInt32(reader["TypeID"])
                        };

                        addressmodellist.Add(addressModel);
                    }
                }
                con.Close();
            }
            return addressmodellist;
        }

        public AddressModel UpdateAddress(int userID, AddressModel address)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(this.connectionString))
                {
                    SqlCommand cmd = new SqlCommand("dbo.usp_Update_AddressDetail", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@TypeID", address.TypeID);
                    cmd.Parameters.AddWithValue("@Address", address.Address);
                    cmd.Parameters.AddWithValue("@City", address.City);
                    cmd.Parameters.AddWithValue("@State", address.State);


                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            address.AddressID = reader.GetInt32(0);
                            address.FullName = reader.GetString(2);
                            address.PhoneNumber = reader.GetInt64(3);
                            address.Address = reader.GetString(4);
                            address.City = reader.GetString(5);
                            address.State = reader.GetString(6);
                            address.TypeID = reader.GetInt32(7);

                        }
                    }
                    con.Close();
                }
                return address;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
