using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IFeedbackManager
    {
        public FeedbackModel AddFeedback(FeedbackModel feedback, int UserID);

        public IEnumerable<FeedbackModel> GetFeedbacks(int BookID);
    }
}
