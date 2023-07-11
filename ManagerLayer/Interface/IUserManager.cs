﻿using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IUserManager
    {
        public UserModel AddUsers(UserModel users);

        public UserModel Login(UserLogin login);

        public UserModel ForgetPassword(UserForgetPassword forgetPassword);

        public bool ResetPassword(UserResetPassword userResetPassword, string Email);

        public string SendGmail(string to, int UserID);

        public string GenerateJWToken(string Email, int UserID);
    }
}
