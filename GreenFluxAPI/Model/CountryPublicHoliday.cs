using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenFluxAPI.Model
{
    [DataContract(Name = "CountryPublicHoliday")]
    public class CountryPublicHoliday
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public List<PublicHoliday> PublicHolidays { get; set; }
        public List<PublicHoliday> UniqueHolidays { get; set; }

    }
}
