using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class WishModel
    {
        public int WishListID { get; set; }

        public int BookID { get; set; }

        public int UserID { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
