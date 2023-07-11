using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAdminRepository
    {
        public AdminModel LoginAdmin(string AdminEmail, string Password);

        public string GenerateJWToken(string Email, int AdminID);

    }
}
