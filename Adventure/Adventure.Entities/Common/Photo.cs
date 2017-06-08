using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Common
{
   public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Display(Name = "Description")]
        [Required]
        public String Decription { get; set; }

        [Display(Name = "Image Path")]
        public String ImagePath { get; set; }

        [Display(Name = "Thumb Path")]
        public String ThumbPath { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }
       
        public String PerformerId { get; set; }

        
        public String ArticleId { get; set; }


    }
}
