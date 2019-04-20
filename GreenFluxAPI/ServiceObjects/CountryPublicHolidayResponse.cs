using GreenFluxAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GreenFluxAPI.ServiceObjects
{
    [DataContract(Name = "CountryPublicHolidayResponse")]
    public class CountryPublicHolidayResponse : ResponseBase
    {
        [DataMember]
        public List<CountryPublicHoliday> CountryPublicHolidays { get; set; }
    }
}
