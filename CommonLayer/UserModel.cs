using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace CommonLayer
{
    public class UserModel
    {
        public int UserID { get; set; }

        [Required]

        public string FullName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public long PhoneNumber { get; set; }
        
    }
}
