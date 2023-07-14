using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository feedbackRepository;

        public FeedbackManager(IFeedbackRepository feedbackRepository) 
        {
            this.feedbackRepository = feedbackRepository;
        }

        public FeedbackModel AddFeedback(FeedbackModel feedback, int UserID)
        {
            return this.feedbackRepository.AddFeedback(feedback, UserID);
        }

        public IEnumerable<FeedbackModel> GetFeedbacks(int BookID)
        {
            return this.feedbackRepository.GetFeedbacks(BookID);
        }
    }
}
