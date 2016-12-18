using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Listings.Service
{
    [RoutePrefix("api/listings")]
    public class ListingsController : ApiController
    {
        private readonly IListingsRepository _listingsRepository;

        public ListingsController(IListingsRepository listingsRepository)
        {
            _listingsRepository = listingsRepository;
        }

        [HttpGet]
        [Route("")]
        public IList<PublicListing> GetAll()
        {
            return _listingsRepository.GetAll();
        }

        [HttpGet]
        [Route("{id:int}")]
        public PublicListing Get(int id)
        {
            var listing = _listingsRepository.Get(id);

            if (listing == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return listing;
        }

        [HttpPost]
        [Route("")]
        public int Post([FromBody]Listing listing)
        {
            return _listingsRepository.Add(listing);
        }

        /// <summary>
        /// Use POST to create resource. PUT here is for modifications only.
        /// </summary>
        [HttpPut]
        [Route("{id:int}")]
        public void Put(int id, [FromBody]Listing update)
        {
            var listing = _listingsRepository.Get(id);

            if (listing == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _listingsRepository.Update(id, update);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var listing = _listingsRepository.Get(id);

            if (listing == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _listingsRepository.Delete(id);
        }
    }

    public class PublicListing : Listing
    {
        public int Id { get; set; }
    }

    public class Listing
    {
        public string Text { get; set; }
    }
}
