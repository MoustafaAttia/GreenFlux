using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GreenFluxAPI.ServiceObjects
{
    [DataContract(Name = "AnswerResponse")]
    public class AnswerResponse
    {
        [DataMember(Name = "QuestionString")]
        public string QuestionString { get; set; }

        [DataMember(Name = "Answer")]
        public string Answer { get; set; }

        [DataMember(Name = "Explanation")]
        public string Explanation { get; set; }

        [DataMember(Name = "AdditionalInfo")]
        public List<string> AdditionalInfo { get; set; }
    }
}
