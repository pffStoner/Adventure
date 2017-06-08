using Adventure.Entities.Common;
using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Models
{

    public class Adventures

    {

        public Adventures()

        { }



        //public Adventures(string performerId, string eventTopicId, string title, string description, string externalUrl, string imgUrl,

        //    DateTime eventDate, string venueId, DateTime dateCreated, DateTime? dateModified, CustomId id = null)

        //    : this(id)

        //{

        //    this.PerformerId = performerId;

        //    this.Title = title;

        //    this.Description = description;

        //    this.ExternalUrl = externalUrl;

        //    this.ImgUrl = imgUrl;

        //    this.EventDate = eventDate;

        //    this.VenueId = venueId;

        //    this.EventTopicId = eventTopicId;

        //    this.DateCreated = dateCreated;

        //    this.DateModified = dateModified;

        //}



        public Adventures(CustomId id)

        {

            this.Id = string.IsNullOrEmpty(Convert.ToString(id)) ? new CustomId().ToString() : id.ToString();

        }
        public Adventures( string performerId, string eventTopicId, string title, string description, string externalUrl, string imgUrl,

            DateTime eventDate, string venueId, DateTime dateCreated, DateTime? dateModified,  CustomId id = null)

            : this(id)
        {
            this.PerformerId = performerId;

            this.Title = title;

            this.Description = description;

            this.ExternalUrl = externalUrl;

            this.ImgUrl = imgUrl;

            this.EventDate = eventDate;

            this.VenueId = venueId;

            this.EventTopicId = eventTopicId;

            this.DateCreated = dateCreated;

            this.DateModified = dateModified;
            //this.VoteCount = VoteCount;
        }



        [Key]
        public string Id { get; set; }



        [Required]

        [StringLength(255)]

        public string Title { get; set; }



        [Required]

        [StringLength(255)]

        public string Description { get; set; }



        [Required]

        [Display(Name = "Video")]

        public string ExternalUrl { get; set; }



        [Required]

        [Display(Name = "Image")]

        public string ImgUrl { get; set; }



        [Required]

        [Display(Name = "Event date")]

        public DateTime EventDate { get; set; }



        [Required]

        public string PerformerId { get; set; }



        public virtual User Performer { get; set; }



        [Required]
        [Display(Name = "Location")]
        public string VenueId { get; set; }


  //      public virtual Venue Venue { get; set; }



        [Required]
        [Display(Name = "Privacy")]
        public string EventTopicId { get; set; }



        public virtual EventTopic EventTopic { get; set; }



        [Required]
        [Display(Name = "Date created")]

        public DateTime DateCreated { get; set; }



        [Display(Name = "Date modified")]
        public DateTime? DateModified { get; set; }

        public int VoteCount { get; set; }

      
    }

}
