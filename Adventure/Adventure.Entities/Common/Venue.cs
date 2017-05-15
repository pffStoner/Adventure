using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Common
{
   public class Venue
    {
        public Venue()
        { }

        public Venue(string pubName, int capacity, CustomId id = null)
            : this(id)
        {

            this.PubName = pubName;
            this.Capacity = capacity;
        }

        public Venue(CustomId id)

        {
            this.Id = string.IsNullOrEmpty(Convert.ToString(id)) ? new CustomId().ToString() : id.ToString();
        }

        [Key]
        public string Id { get; set; }


        [Required]
        [StringLength(255)]
        public string PubName { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}
