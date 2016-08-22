using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineAssessmentForm.OData
{
    public class ODataQuery : IODataQuery
    {
        public const string FilterParameterName = "filter";
        public const string SelectParameterName = "select";
        public const string ExpandParameterName = "expand";

        private Dictionary<string, string> QueryParts { get;} = new Dictionary<string, string>();
        private object Key { get; set; }

        public string EntityName { get; set; }

        public ODataQuery(string entityName)
        {
            EntityName = entityName;
        }

        public string ToODataUrl()
        {
            //If this is a "by key" query, then this classes Assert should prevent anything but Key from being set
            var url = Key == null ? BuildParameterQuery() : BuildGetByKeyQuery();
            ClearQuery();
            return url;
        }

        private string BuildGetByKeyQuery()
        {
            return $"{EntityName}({Key})";
        }

        private string BuildParameterQuery()
        {
            var builder = new StringBuilder(EntityName);

            var first = QueryParts.First();
            builder.Append($"?${first.Key}={first.Value}");

            foreach (var queryPart in QueryParts.Skip(1))
            {
                builder.Append($"&${queryPart.Key}={queryPart.Value}");
            }

            return builder.ToString();
        }

        private void ClearQuery()
        {
            this.QueryParts.Clear();
            this.Key = null;
        }

        public void GetByKey(object key)
        {
            AssertOnlyOne();
            Key = key;
        }

        public IODataQuery Filter(string queryString)
        {
            return AddToQuery(FilterParameterName, queryString);
        }

        public IODataQuery Select(string queryString)
        {
            return AddToQuery(SelectParameterName, queryString);
        }

        public IODataQuery Expand(string queryString)
        {
            return AddToQuery(ExpandParameterName, queryString);
        }

        //todo: provide multiple types of parameters that can be added. not all will be a <param>=<query> syntax, they could be functions
        private IODataQuery AddToQuery(string parameterName, string queryString)
        {
            AssertOnlyOne(parameterName);
            QueryParts[parameterName] = queryString;

            return this;
        }

        private void AssertOnlyOne(string parameter = "")
        {
            if (parameter == "")
                if (QueryParts.Any() || Key != null)
                    throw new InvalidOperationException($"This option must be applied by itself to an OData query"); //todo: fix useless error message

            if (QueryParts.ContainsKey(parameter))
                throw new InvalidOperationException($"Only one {parameter} may be applied to an OData query");
            if (Key != null)
                throw new InvalidOperationException("This query already contains a GetByKey constraint, which is only valid by itself"); //todo: here as well
        }
    }
}