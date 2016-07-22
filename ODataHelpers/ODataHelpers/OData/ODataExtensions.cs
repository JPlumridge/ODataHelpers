using System;
using System.Text;

namespace OnlineAssessmentForm.OData
{
    public static class ODataExtensions
    {
        public static IODataQuery FilterByEquals(this IODataQuery query, string propertyName, string value)
        {
            return query.Filter($"{propertyName} eq {value.ToFormattedODataValue()}");
        }

        public static IODataQuery SelectProperties(this IODataQuery query, params string[] properties)
        {
            var builder = new StringBuilder(properties[0]);
            
            foreach (var property in properties)
            {
                builder.Append($",{property}");
            }

            return query.Select(builder.ToString());
        }

        public static string ToFormattedODataValue(this string value)
        {
            value = $"'{Uri.EscapeDataString(value)}'";
            //OData doesn't like %27 in it's query strings. Replacing with a double ' is the correct solution
            value = value.Contains("%27") ? value.Replace("%27", "''") : value;

            return value;
        }
    }
}