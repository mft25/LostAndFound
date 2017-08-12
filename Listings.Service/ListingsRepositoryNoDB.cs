using System;
using System.Collections.Generic;

namespace Listings.Service
{
    public class ListingsRepositoryNoDB : IListingsRepository
    {
        public PublicListing Get(int id)
        {
            if (id == 1234)
            {
                return new PublicListing
                {
                    Id = 1234,
                    Text = "test text"
                };
            }

            return null;
        }

        public IList<PublicListing> GetAll()
        {
            return new List<PublicListing>
            {
                new PublicListing
                {
                    Id = 1234,
                    Text = "test text"
                }
            };
        }

        public void Update(int id, Listing listing)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Add(Listing listing)
        {
            throw new NotImplementedException();
        }
    }
}
