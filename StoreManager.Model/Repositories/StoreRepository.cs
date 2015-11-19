using System;
using System.Collections.Generic;
using StoreManager.Model.Models;
using StoreManager.Model.Endpoints;
using System.Net.Http;

namespace StoreManager.Model.Repositories
{
    public class StoreRepository : BaseRepository<StoreResource>, IStoreRepository
    {
        public StoreRepository(IStoreManagerEndpoint endpoint)
            :base(endpoint) { }

        protected override string Controller
        {
            get { return "Stores"; }
        }

        public IEnumerable<PartResource> GetStockedParts(StoreResource store)
        {
            return Manager.Execute<PartResource>($"{Controller}/Parts", HttpMethod.Get);
        }
    }
}
