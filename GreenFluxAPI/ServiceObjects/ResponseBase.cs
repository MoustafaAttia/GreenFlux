using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GreenFluxAPI.ServiceObjects
{
    [DataContract(Name = "ResponseBase")]
    public class ResponseBase
    {
        [DataMember(Name = "Status")]
        public bool Status { get; set; }
        [DataMember(Name = "ExceptionMessage")]
        public string ExceptionMessage { get; set; }
    }
}
