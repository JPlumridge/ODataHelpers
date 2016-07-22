using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OnlineAssessmentForm.OData
{
    public class ODataFunction : IODataFunction
    {
        public string EntityName { get; set; }
        public string FunctionNamespace { get; set; }
        public string FunctionName { get; set; }
        public Dictionary<string, object> FunctionParameters { get; set; }
        public string ToODataUrl()
        {
            var builder = new StringBuilder(EntityName);

            builder.Append("/").Append(FunctionNamespace)
                .Append(".").Append(FunctionName)
                .Append("(");

            foreach (var kvp in FunctionParameters)
            {
                //todo: move OData specific url encoding to somewhere else
                var name = Uri.EscapeDataString(kvp.Key);
                var value = kvp.Value;
                if (value is string)
                {
                    value = ((string)value).ToFormattedODataValue();
                }
                builder.Append(name).Append("=").Append(value).Append(",");
            }
            var url = builder.ToString().TrimEnd(',');
            return url + ")";
        }
    }
}