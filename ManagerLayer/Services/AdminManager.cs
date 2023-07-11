using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ManagerLayer.Services
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepository adminRepository;

        public AdminManager(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public AdminModel LoginAdmin(string AdminEmail, string Password)
        {
            return  adminRepository.LoginAdmin(AdminEmail , Password);
        }

        public string GenerateJWToken(string Email, int AdminID)
        {
            return adminRepository.GenerateJWToken(Email, AdminID);
        }

        public string SendGmail(string to, int AdminID)
        {  
            try
            {
                string token = GenerateJWToken(to, AdminID);

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

        
    }
}
