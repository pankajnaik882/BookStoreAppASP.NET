using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IAdminManager
    {
        public AdminModel LoginAdmin(string AdminEmail, string Password);

        public string GenerateJWToken(string Email, int AdminID);

        public string SendGmail(string to, int AdminID);

    }
}
