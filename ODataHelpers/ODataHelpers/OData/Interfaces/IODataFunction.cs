using System.Collections.Generic;

namespace OnlineAssessmentForm.OData
{
    public interface IODataFunction : IODataUrlProvider
    {
        string FunctionNamespace { get; set; }
        string FunctionName { get; set; }
        Dictionary<string, object> FunctionParameters { get; set; }
    }
}