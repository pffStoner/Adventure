using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Common
{
    public sealed class EventTopic
    {
        public EventTopic()

        { }

        public EventTopic(string name, CustomId id = null)
            : this(id)
        {

            this.Name = name;
        }

        public EventTopic(CustomId id)

        {
            this.Id = string.IsNullOrEmpty(Convert.ToString(id)) ? new CustomId().ToString() : id.ToString();
       }
        
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Event Topic")]
        public string Name { get; set; }
    }
}
