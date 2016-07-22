namespace OnlineAssessmentForm.OData
{
    public interface IODataUrlProvider
    {
        string EntityName { get; set; }
        string ToODataUrl();
    }
}