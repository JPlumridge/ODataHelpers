namespace OnlineAssessmentForm.OData
{
    public interface IODataQuery : IODataUrlProvider
    {
        IODataQuery Filter(string queryString);
        IODataQuery Select(string queryString);
        IODataQuery Expand(string queryString);
        void GetByKey(object key);
    }
}