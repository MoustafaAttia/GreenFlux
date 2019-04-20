using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenFluxAPI.Model
{
    [DataContract(Name = "PublicHolidays")]
    public class PublicHoliday
    {
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "localName")]
        public string LocalName { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "countryCode")]
        public string CountryCode { get; set; }

        [DataMember(Name = "fixed")]
        public bool Fixed { get; set; }

        [DataMember(Name = "global")]
        public bool Global { get; set; }

        [DataMember(Name = "counties")]
        public List<string> Counties { get; set; }

        [DataMember(Name = "launchYear")]
        public string LaunchYear { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
