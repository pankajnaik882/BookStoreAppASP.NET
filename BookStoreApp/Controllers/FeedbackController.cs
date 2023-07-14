using CommonLayer;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager feedbackManager;

        public FeedbackController(IFeedbackManager feedbackManager)
        {
            this.feedbackManager = feedbackManager;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddFeedback")]

        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try 
            {
                int User = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                FeedbackModel feedbackModelData = this.feedbackManager.AddFeedback(feedbackModel, User);

                if (feedbackModelData != null)
                {
                    return this.Ok(new { success = true, message = "Feedback Added Successfully" ,result = feedbackModelData });
                }

                return this.BadRequest(new { success = true, message = "Process Failed" });
            }

            catch (Exception ex)
            {
                return this.NotFound(new { success = false, result = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet]
        [Route("GetAllFeedback")]

        public IActionResult GetFeedbacks(int BookID) 
        {
            try
            {
                List<FeedbackModel> feedbackListData = (List<FeedbackModel>)this.feedbackManager.GetFeedbacks(BookID);
                if(feedbackListData != null)
                {
                    return this.Ok(new { success = true, message = "Fetched list of Feedback", result = feedbackListData });
                }

                return this.BadRequest(new { success = true, message = "Process Failed" });
            }

            catch (Exception )
            {
                return this.NotFound(new {Success = false });
            }
        }
    }
}
