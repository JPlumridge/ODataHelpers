using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace OnlineAssessmentForm.OData
{
    public class HttpODataClient : IODataClient
    {
        private HttpClient HttpClient { get; set; }
        private Uri ServiceRoot { get; set; }

        public HttpODataClient(Uri serviceRoot) : this(serviceRoot, string.Empty)
        {
        }

        public HttpODataClient(Uri serviceRoot, string sharedKey)
        {
            if (!serviceRoot.IsAbsoluteUri)
                throw new ArgumentException("serviceRoot must be an absolute Uri", nameof(serviceRoot));
            ServiceRoot = serviceRoot;

            HttpClient = new HttpClient();

            if (!string.IsNullOrWhiteSpace(sharedKey))
                AddSharedKey(sharedKey);
        }

        public void AddSharedKey(string sharedKey)
        {
            HttpClient.DefaultRequestHeaders.Add("SharedKey", sharedKey);
        }

        public async Task<IEnumerable<T>> GetCollectionQuery<T>(IODataUrlProvider query)
        {
            var fullUri = GetFullUri(query);
            
            var result = await HttpClient.GetStringAsync(fullUri);
            return JsonConvert.DeserializeObject<ODataCollection<T>>(result).Value;
        }

        public async Task<T> GetSingleQuery<T>(IODataUrlProvider query)
        {
            var fullUri = GetFullUri(query);

            var result = await HttpClient.GetStringAsync(fullUri);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private Uri GetFullUri(IODataUrlProvider urlProvider)
        {
            var endpoint = urlProvider.ToODataUrl();
            return new Uri(ServiceRoot.OriginalString + endpoint);
        }
    }
}