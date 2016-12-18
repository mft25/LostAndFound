using System.Collections.Generic;

namespace Listings.Service
{
    public interface IListingsRepository
    {
        PublicListing Get(int id);
        IList<PublicListing> GetAll();
        int Add(Listing listing);
        void Update(int id, Listing listing);
        void Delete(int id);
    }
}
