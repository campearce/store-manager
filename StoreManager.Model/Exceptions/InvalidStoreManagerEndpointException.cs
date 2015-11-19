using StoreManager.Model.Endpoints;
using System;

namespace StoreManager.Model.Exceptions
{
    public class InvalidStoreManagerEndpointException : Exception
    {
        public InvalidStoreManagerEndpointException(IStoreManagerEndpoint endpoint)
            :base(endpoint.ValidationFailMessage, null)
        { }
    }
}
