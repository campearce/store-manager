using System.Collections.Generic;
using System.Net.Http;

namespace StoreManager.Model.Endpoints
{
    public interface IStoreManagerEndpoint
    {
        string ValidationFailMessage { get; set; }
        bool IsValid { get; }

        IEnumerable<T> Execute<T>(string endpoint, HttpMethod method) where T : class;
        IEnumerable<T> Execute<T>(string endpoint, HttpMethod method, object body) where T : class;
    }
}
