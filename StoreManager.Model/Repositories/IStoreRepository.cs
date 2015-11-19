using System.Collections.Generic;
using StoreManager.Model.Models;

namespace StoreManager.Model.Repositories
{
    public interface IStoreRepository : IGet<StoreResource>
    {
        IEnumerable<PartResource> GetStockedParts(StoreResource store);
    }
}
