using StoreManager.Model.Models;
using StoreManager.Model.Endpoints;
using StoreManager.Model.Exceptions;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System;

namespace StoreManager.Model.Repositories
{
    public abstract class BaseRepository<TResource> where TResource : class, IResource
    {
        protected IStoreManagerEndpoint Manager { get; set; }
        protected abstract string Controller { get; }

        public BaseRepository(IStoreManagerEndpoint endpoint)
        {
            if (!endpoint.IsValid)
            {
                throw new InvalidStoreManagerEndpointException(endpoint);
            }

            Manager = endpoint;
        }

        public IEnumerable<TResource> All()
        {
            return Manager.Execute<TResource>($"{Controller}/All", HttpMethod.Get);
        }

        public IEnumerable<TResource> Find(Func<TResource, bool> predicate)
        {
            return All().Where(predicate);
        }

        public TResource GetByName(string name)
        {
            var many = Manager.Execute<TResource>($"{Controller}/All", HttpMethod.Post, new
            {
                Name = name
            });

            return many.FirstOrDefault();
        }

        public TResource Get(string id)
        {
            var many = Manager.Execute<TResource>($"{Controller}/{id}", HttpMethod.Get);
            return many.FirstOrDefault();
        }
    }
}
