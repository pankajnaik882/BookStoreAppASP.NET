using CommonLayer;
using ManagerLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager adminManager;

        private readonly IBusControl _bus;

        public AdminController(IAdminManager adminManager, IBusControl bus)
        {
            this.adminManager = adminManager;
            _bus = bus;
        }

        
        [HttpPost]
        [Route("AdminLogin")]
        public async Task<IActionResult> LoginAdmin(string Email , string Password)
        {
            try
            {
                AdminModel AdminData = this.adminManager.LoginAdmin(Email , Password);

                this.adminManager.SendGmail(AdminData.Email, AdminData.AdminID);

                Uri uri = new Uri("rabbitmq://localhost/BookStoreAdmin-Queue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(AdminData);

                if (AdminData != null)
                {
                    return this.Ok(new { Success = true, message = "Email Sent Successfull", result = AdminData });
                }

                return this.BadRequest(new { success = true, message = "Verification Failed" });
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
