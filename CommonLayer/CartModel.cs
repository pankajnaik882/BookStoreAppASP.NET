using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class CartModel
    {
        public int CartID { get; set; }

        public int BookID { get; set; }


        public int UserID { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
