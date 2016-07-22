using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnlineAssessmentForm.OData
{
    public class ODataCollection<T>
    {
        [JsonProperty("odata.context")]
        public string Metadata { get; set; }
        public List<T> Value { get; set; } //todo: change to IEnumerable
    }
}