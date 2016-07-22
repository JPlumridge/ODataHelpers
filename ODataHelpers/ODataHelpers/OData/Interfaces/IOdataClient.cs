using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAssessmentForm.OData
{
    public interface IODataClient
    {
        Task<IEnumerable<T>> GetCollectionQuery<T>(IODataUrlProvider query);
        Task<T> GetSingleQuery<T>(IODataUrlProvider query);
    }
}