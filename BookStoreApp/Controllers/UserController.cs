using CommonLayer;
using ManagerLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStoreApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        private readonly IBusControl _bus;

        public UserController(IUserManager userManager, IBusControl bus)
        {
            this.userManager = userManager;
            _bus = bus;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IActionResult Registration(UserModel userRegisterationModel)
        
        {
            try
            {
                UserModel registerationData = this.userManager.AddUsers(userRegisterationModel);

                if (registerationData != null)
                {
                    return this.Ok(new { Success = true, message = "Registration Successful", result = registerationData });
                }

                return this.BadRequest(new { success = true, message = "User Already Exists" });

            }
            catch (Exception)
            {
                return this.NotFound(new { success = false });
            }
        }

        
        [HttpGet]
        [Route("Login")]
   
        public IActionResult Login(UserLogin userLoginModel)
        {
            try
            {
                UserModel UserLoginData = this.userManager.Login(userLoginModel);

                if (UserLoginData != null)
                {
                    
                    return this.Ok(new { Success = true, message = "Login Successfull", result = UserLoginData });
                }

                return this.BadRequest(new { success = true, message = "Login Failed" });

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(UserForgetPassword userForgetModel)
        {
            try
            {
                UserModel UserForgetData = this.userManager.ForgetPassword(userForgetModel);

                this.userManager.SendGmail(UserForgetData.Email, UserForgetData.UserID);

                Uri uri = new Uri("rabbitmq://localhost/BookStore-Queue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(UserForgetData);

                if (UserForgetData != null)
                {
                    return this.Ok(new { Success = true, message = "Email Sent Successfull", result = UserForgetData });
                }

                return this.BadRequest(new { success = true, message = "Fetching Failed" });
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]

        public IActionResult ResetPassword(UserResetPassword resetPassword )
        {
            try
            {
                string Email = this.User.FindFirst("Email").Value;

                if (resetPassword.Password == resetPassword.ConfirmPassword)
                {
                    bool userPassword = this.userManager.ResetPassword(resetPassword, Email);
                    if (userPassword)
                    {
                        return this.Ok(new { Success = true, message = "Password Reset Successfull", result = resetPassword });
                    }
                }

                return this.BadRequest(new { success = true, message = "Fetching Failed" });
            }

            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }

        }

        
    }
}
