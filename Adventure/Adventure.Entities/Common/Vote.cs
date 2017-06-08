using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Common
{
    public class Vote
    {
        public Vote()
        { }

        public Vote(int vote,string performerID, CustomId id = null)
            : this(id)
        {

            this.VoteUp = vote;
            this.PerformerId = performerID;
            // this.Capacity = capacity;
           // this.ArticleId = articleId;
        }

        public Vote(CustomId id)

        {
            this.VoteID = string.IsNullOrEmpty(Convert.ToString(id)) ? new CustomId().ToString() : id.ToString();
        }

        [Key]
        public string VoteID { get; set; }


        

        [Required]
        public int VoteUp { get; set; }

        [Required]
        public string PerformerId { get; set; }

        //[Required]
        //public string ArticleId { get; set; }
    }
}
