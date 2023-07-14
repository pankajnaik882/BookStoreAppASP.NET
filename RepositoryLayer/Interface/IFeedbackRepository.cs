using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRepository
    {
        public FeedbackModel AddFeedback(FeedbackModel feedback, int UserID);

        public IEnumerable<FeedbackModel> GetFeedbacks(int BookID);
    }
}
