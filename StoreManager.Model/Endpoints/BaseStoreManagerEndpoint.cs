using System.Collections.Generic;
using System.Net.Http;

namespace StoreManager.Model.Endpoints
{
    public abstract class BaseStoreManagerEndpoint : IStoreManagerEndpoint
    {
        public bool IsValid
        {
            get
            {
                return Validate();
            }
        }

        protected abstract bool Validate();
        public abstract string ValidationFailMessage { get; set; }

        public abstract IEnumerable<T> Execute<T>(string endpoint, HttpMethod method) where T : class;
        public abstract IEnumerable<T> Execute<T>(string endpoint, HttpMethod method, object body) where T: class;
    }
}
