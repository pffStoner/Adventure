using Project.Common.attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Adventure.DTOs
{
    public class AdventuresEntry
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Video Url")]
        public string ExternalUrl { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }

        [Required]
        [StringLength(50)]
         [FutureDate]
        public string Date { get; set; }

        [Required]
        [StringLength(50)]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public string VenueId { get; set; }

        [Required]
        [Display(Name = "Privacy")]
        public string EventTopicId { get; set; }
        public int VoteCount { get; set; }
        public DateTime GetDate()
        {
            return DateTime.Parse($"{this.Date} {this.Time}");
        }
    }
}