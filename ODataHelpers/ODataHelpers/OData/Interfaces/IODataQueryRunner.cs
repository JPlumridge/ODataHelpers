using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAssessmentForm.OData
{
    public interface IODataQueryRunner<T>
    {
        IODataQuery Query { get; }
        Task<IEnumerable<T>> GetCollection(Action<IODataQuery> queryBuilderFunc);  //could be func?
        Task<T> GetByKey(object key);
    }
}