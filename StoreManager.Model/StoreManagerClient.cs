using StoreManager.Model.Endpoints;
using StoreManager.Model.Repositories;

namespace StoreManager.Model
{
    public class StoreManagerClient
    {
        public StoreManagerClient()
            :this(new JsonFilesStoreManagerEndpoint())
        {
        }

        public StoreManagerClient(IStoreManagerEndpoint endpoint)
        {
            Stores = new StoreRepository(endpoint);
        }

        public IStoreRepository Stores { get; set; }


    }
}
