using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenFluxAPI.Model
{
    [DataContract(Name = "CountryPublicHoliday")]
    public class CountryPublicHoliday
    {
        [DataMember(Name = "CountryName")]
        public string CountryName { get; set; }

        [DataMember(Name = "CountryCode")]
        public string CountryCode { get; set; }

        [DataMember(Name = "PublicHolidays")]
        public List<PublicHoliday> PublicHolidays { get; set; }

        [DataMember(Name = "UniqueHolidays")]
        public List<PublicHoliday> UniqueHolidays { get; set; }

    }
}
