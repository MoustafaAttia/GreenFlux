using GreenFluxAPI.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenFluxAPI.ServiceObjects
{
    [DataContract(Name = "PublicHolidayResponse")]
    public class PublicHolidayResponse : ResponseBase
    {
        [DataMember]
        public List<PublicHoliday> PublicHolidays { get; set; }
    }
}
