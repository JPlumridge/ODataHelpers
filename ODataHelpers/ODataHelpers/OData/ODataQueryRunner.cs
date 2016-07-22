using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineAssessmentForm.OData
{
    public class ODataQueryRunner<TResult> : IODataQueryRunner<TResult>
    {
        public IODataClient Client { get; }
        public IODataQuery Query { get; }

        public ODataQueryRunner(IODataClient client, string entityName)
        {
            Client = client;
            Query = new ODataQuery(entityName);

        }
        public async Task<IEnumerable<TResult>> GetCollection(Action<IODataQuery> queryBuilderFunc)
        {
            queryBuilderFunc(Query);
            return await Client.GetCollectionQuery<TResult>(Query);
        }

        public async Task<TResult> GetByKey(object key)
        {
            Query.GetByKey(key);
            return await Client.GetSingleQuery<TResult>(Query);
        }
    }
}