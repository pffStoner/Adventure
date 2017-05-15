using Project.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure.Entities.Common
{
    public class Location
    {
        public Location(string ip, string countryCode, string countryName, string City, 
            string zipCode, string latitude, string longitude,

            string regionCode = null, string regionName = null, 
            string dateZone = null, string metroCode = null, CustomId id = null)

            : this(regionCode, regionName, dateZone, metroCode, id)

        { }

        public Location(string regionCode, string regionName, string dateZone, string metroCode, CustomId id)

        {
            this.Id = string.IsNullOrEmpty(Convert.ToString(id)) ? new CustomId().ToString() : id.ToString();
            this.RegionCode = regionCode;
            this.RegionName = regionName;
            this.DateZone = dateZone;
            this.MetroCode = metroCode;
        }

        [Key]
        public string Id { get; set; }

        public string IP { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public string RegionCode { get; set; }

        public string RegionName { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string DateZone { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string MetroCode { get; set; }
    }
}
