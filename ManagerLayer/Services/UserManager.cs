using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ManagerLayer.Services
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserModel AddUsers(UserModel users)
        {
            return userRepository.AddUsers(users);
        }

        public UserModel Login(UserLogin login)
        {
            return userRepository.Login(login);
        }

        public UserModel ForgetPassword(UserForgetPassword forgetPassword)
        {
            return userRepository.ForgetPassword(forgetPassword);
        }

        public string GenerateJWToken(string Email, int UserID)
        {
            return userRepository.GenerateJWToken(Email, UserID);
        }

        public string SendGmail(string to, int UserID)
        {
            /*string to = "fagebi2588@anwarb.com";*/ //To address    
            try
            {
                string token = GenerateJWToken(to, UserID);

                string from = "pankaj080519999@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);

                string mailbody = "TOKEN GENERATED : " + token;
                message.Subject = "Your Secret Token Generated";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("pankaj080519999@gmail.com", "zpxpakwjxzcxwuqf");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);

                return to;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ResetPassword(UserResetPassword userResetPassword, string Email)
        {
            return userRepository.ResetPassword(userResetPassword, Email);
        }
    }
}
