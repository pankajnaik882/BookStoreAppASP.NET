using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer
{
    public class BookModel
    {
        public int BookID { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public int AuthorID { get; set; }

        public string Description { get; set; }

        public string BookImage { get; set; }

        [Required]
        public int InStock { get; set; }

        [Required]
        public int Price { get; set; }

        public int DiscountPrice { get; set; } 
									
        public int TotalRating { get; set; } 
									
        public int RatingCount { get; set; } 
									
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        
    }
}
