using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        public int UserID { get; set; } 

        public int BookID { get; set; }

        public int AddressID { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }
    }
}
