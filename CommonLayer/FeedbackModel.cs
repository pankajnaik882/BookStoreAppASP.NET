using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class FeedbackModel
    {
        public int FeedbackID { get; set; }

        public int UserID { get; set; }

        public int BookID { get; set; }

        public string Review { get; set; }

        public string Comment { get; set; }
    }
}
