using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Common
{
    public class MailingAdress
    {
        public MailingAdress(string postCode, string houseNumber, string street, string town, CustomId id = null)
          : this(id)
        {

            this.PostCode = postCode;
            this.HouseNumber = houseNumber;
            this.Street = street;
            this.Town = town;

        }



        public MailingAdress(CustomId id)

        {
            this.Id = string.IsNullOrEmpty(Convert.ToString(id)) ? new CustomId().ToString() : id.ToString();
        }



        [Key]
        public string Id { get; set; }

        [Required]
        public string PostCode { get; private set; }

        [Required]
        public string HouseNumber { get; private set; }

        [Required]
        public string Street { get; private set; }

        [Required]
        public string Town { get; private set; }
    }
}
